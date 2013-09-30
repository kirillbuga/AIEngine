using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;

namespace Visualization
{
    static class Extensions
    {
        public static void Initialization(this SimpleOpenGlControl control)
        {
            // инициализация библиотеки GLUT 
            Glut.glutInit();
            // инициализация режима окна 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            // устанавливаем цвет очистки окна 
            Gl.glClearColor(255, 255, 255, 1);

            // устанавливаем порт вывода, основываясь на размерах элемента управления AnT 
            Gl.glViewport(0, 0, control.Width, control.Height);

            // устанавливаем проекционную матрицу 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очищаем ее 
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, control.Width, 0.0, control.Height);

            // переходим к объектно-видовой матрице 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_BLEND);
            //Gl.glEnable(Gl.GL_DEPTH_TEST);
            //Gl.glEnable(Gl.GL_COLOR_MATERIAL);
        }
    }
}
