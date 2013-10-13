using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AIEngine.Entities;
using AIEngine.GeneticAlgorithmImplementation;
using AIEngine.GeneticAlgorithmImplementation.NeuroGeneticAlgorithm;
using GeneticAlgorithm.Implementation.Common;
using NeuralNetworkCore;
using Tao.FreeGlut;
using Tao.OpenGl;
using AIEngine;

namespace Visualization
{
    public partial class MainForm : Form
    {
        private const int AgentWidth = 10;
        public Random Random { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }
        public bool IsGeneticNow { get; set; }
        public GameEnvironment GameEnvironment { get; set; }
        public NeuroGeneticAlgorithm GeneticAlgorithm { get; set; }

        private bool _isDebugMode = false;

        public MainForm()
        {
            Random = new Random((int) DateTime.Now.Ticks);

            InitializeComponent();
            ant.InitializeContexts();
            ant.Initialization();
            
            refreshTimer.Start();

            NeuralNetwork = new NeuralNetwork()
                .WithInputs(8)
                .WithLayerWithSeparateActivationFunction(2)
                .WithLayerWithSeparateActivationFunction(3)
                .WithOutputs(1);

            var options = new EnvironmentOptions
            {
                AgentsCount = 20,
                FoodCount = 30,
                FieldWidth = ant.Width,
                FieldHeight = ant.Height,
                NeuralNetwork = NeuralNetwork
            };

            GameEnvironment = new GameEnvironment(options);

            var fittnessFunction = new NeuroFittnessFunction();
            var selection = new RouletteSelection<Neuron>();
            var crossover = new ComplexNeuroCrossover(3, true, true);
            var mutation = new NeuroMutation(2, 5);
            var terminate = new NeuroTerminate();

            GeneticAlgorithm = new NeuroGeneticAlgorithm(fittnessFunction, selection, crossover, mutation, terminate,
                                                         NeuralNetwork);
        }

        #region OpenGl

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            UpdateEnvironment();
            Draw();

            //if (isGridVisible.Checked)
            //{
            //    DrawLines();
            //}

            Gl.glFlush();
            ant.Invalidate();
        }

        private void DrawLines()
        {
            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor4d(0, 0, 0, 0.2);
            for (int i = 0; i < ant.Width; i += AgentWidth)
            {
                Gl.glVertex2d(i, 0);
                Gl.glVertex2d(i, ant.Height);
            }

            for (int j = 0; j < ant.Height; j += AgentWidth)
            {
                Gl.glVertex2d(ant.Width, j);
                Gl.glVertex2d(0, j);
            }

            Gl.glEnd();
        }

        private void UpdateEnvironment()
        {
            GameEnvironment.CalculateAgentsEnvironmentParameters();
            GameEnvironment.GetHarvestedFood();
        }

        private void Draw()
        {
            foreach (var food in GameEnvironment.Foods)
            {
                DrawFood(food);
            }
            foreach (var agent in GameEnvironment.Agents)
            {
                DrawAgent(agent);
                agent.Move(GameEnvironment.CanMove(agent));
            }
        }

        private void DrawFood(Food food)
        {
            Gl.glColor3d(255, 0, 0);
            DrawCircle(food.X, food.Y, AgentWidth/2);
        }

        private void DrawAgent(EnvironmentAgent agent)
        {
            Gl.glColor3b(agent.Color.R, agent.Color.G, agent.Color.B);
            DrawCircle(agent.X, agent.Y, AgentWidth);

            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2d(agent.X, agent.Y);
            Gl.glVertex2d(agent.X + agent.Vector.X * AgentWidth, agent.Y + agent.Vector.Y * AgentWidth);
            Gl.glEnd();

            if (_isDebugMode)
            {
                Gl.glPushMatrix();
                drawBitmapText_(agent.Angle.ToString(), agent.X, agent.Y);
                Gl.glPopMatrix();
            }
        }

        private void drawBitmapText_(string str,double x,double y) 
        {  
            Gl.glRasterPos2d(x, y);
            foreach (var s in str)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_10, s);
            }
        }

        private void DrawCircle(double cx, double cy, double radius)
        {
            Gl.glBegin(Gl.GL_LINE_LOOP);
            for (int i = 0; i <= 300; i++)
            {
                var angle = 2 * Math.PI * i / 300;
                var x = Math.Cos(angle);
                var y = Math.Sin(angle);
                Gl.glVertex2d(cx + x * radius, cy + y * radius);
            }
            Gl.glEnd();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            var thread = new Thread(Test);
            thread.Start();
        }

        private void Test()
        {
            for (var i = 0; i <= 10; i++)
            {
                GeneticAlgorithm.PerformIteration();
            }

            var winner = GeneticAlgorithm.Population.FirstOrDefault(
                y => y.FittnessValue == GeneticAlgorithm.Population.Max(x => x.FittnessValue));
            var w = winner as NeuroChromosome;

            NeuralNetwork.SetNetworkState(w);
        }
    }
}
