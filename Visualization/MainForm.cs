using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Entities;
using Tao.OpenGl;
using System.Linq;

namespace Visualization
{
    public partial class MainForm : Form
    {
        private const int CellWidth = 10;
        public Random Random { get; set; }
        public List<Agent> Agents { get; set; }
        public List<Food> Foods { get; set; }
        public NeuralNetwork.NeuralNetwork NeuralNetwork { get; set; }
        public bool IsResult { get; set; }


        public MainForm()
        {
            Random = new Random((int) DateTime.Now.Ticks);

            InitializeComponent();
            ant.InitializeContexts();
            ant.Initialization();

            Agents = new List<Agent>();
            Foods = new List<Food>();

            for (int i = 0; i < 30; i++)
            {
                var agent = new Agent(Random.Next(0, ant.Width), Random.Next(0, ant.Height), Random.Next(0, 360),
                                      Color.Aqua);
                Agents.Add(agent);
            }

            for (int i = 0; i < 30; i++)
            {
                var food = new Food(Random.Next(0, ant.Width), Random.Next(0, ant.Height));
                Foods.Add(food);
            }

            refreshTimer.Start();

            // nn

            NeuralNetwork =
                new NeuralNetwork.NeuralNetwork().WithInputs(3)
                                                 .WithLayerWithSeparateActivationFunction(2)
                                                 .WithLayerWithSeparateActivationFunction(30)
                                                 .WithOutputs(3);
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            //trainButton_Click(sender, e);
            graph.Series[0].Points.Clear();
            graph.Series[1].Points.Clear();

            for (double i = -10; i < 10; i+= 0.1)
            {
                graph.Series[0].Points.AddXY(i, Math.Sin(i));
            }

            for (double i = -10; i < 10; i += 0.1)
            {
                graph.Series[1].Points.AddXY(i, NeuralNetwork.Activate(new List<double>{i}).First());
            }

            textBox1.Text = NeuralNetwork.GetSummaryError().ToString();
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            for (double i = -100; i < 100; i += 0.1)
            {
                var value = Random.Next(0, 360);
                NeuralNetwork.Activate(new List<double> { value });
                NeuralNetwork.Correct(new List<double> { Math.Sin(value) });
            }
            displayButton_Click(null, null);
        }

        private void result_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            nnStructure.Clear();
            nnStructure.Text = NeuralNetwork.GetNetworkState();
        }

        public double I = 0.1;

        private void button3_Click(object sender, EventArgs e)
        {
            I += 0.1;
            NeuralNetwork.Activate(new List<double> { I });
            NeuralNetwork.Correct(new List<double> { I*I });
            result_Click(sender, e);
        }

        #region OpenGl

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

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
            for (int i = 0; i < ant.Width; i += CellWidth)
            {
                Gl.glVertex2d(i, 0);
                Gl.glVertex2d(i, ant.Height);
            }

            for (int j = 0; j < ant.Height; j += CellWidth)
            {
                Gl.glVertex2d(ant.Width, j);
                Gl.glVertex2d(0, j);
            }

            Gl.glEnd();
        }

        private void Draw()
        {
            Gl.glColor3d(255, 0, 0);
            foreach (var food in Foods)
            {
                Gl.glRectd(food.X, food.Y, food.X + CellWidth/2, food.Y + CellWidth/2);
            }

            Gl.glColor3d(0, 255, 0);
            foreach (var agent in Agents)
            {
                Gl.glRectd(agent.X, agent.Y, agent.X + CellWidth, agent.Y + CellWidth);
                var result2 = NeuralNetwork.Activate(new List<double> { agent.X, agent.Y, agent.Angle });
                agent.Angle = result2[2];
                agent.Move();
                if (agent.X > ant.Width || agent.X < 0 || agent.Y < 0 || agent.Y > ant.Height)
                {
                    NeuralNetwork.Activate(new List<double> {agent.X, agent.Y, agent.Angle});
                    agent.Angle = Random.Next(0, 360);
                }
            }

            GetHarvestedFood();
        }

        private void GetHarvestedFood()
        {
            var harvested = (from food in Foods
                            from agent in Agents
                             where GetDistance(food.X, food.Y, agent.X, agent.Y) < CellWidth
                            select food).ToList();

            for (int i = 0; i < harvested.Count; i++)
            {
                Foods.Remove(harvested[i]);
            }
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + (Math.Pow(y2 - y1, 2)));
            //return Math.Abs(x1 - x2) < 10 && Math.Abs(y1 - y2) < 10;
        }

        #endregion
    }
}
