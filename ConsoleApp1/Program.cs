using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DoEventOne one = new DoEventOne();
            DoEventTwo two = new DoEventTwo();
            DoEventThree three = new DoEventThree();

            TimerExcuter actTimer = new TimerExcuter();

            actTimer.TimerEvent += one.DoTimer;
            actTimer.TimerEvent += two.DoTimer;
            actTimer.TimerEvent += three.DoTimer;

            //Form1 f = new Form1();
            //f.ShowDialog();
            //f.Show();

            actTimer.Start();

            System.Threading.Thread t = new System.Threading.Thread(FuncStart);
            t.Start();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("I am Main Loop");
                System.Threading.Thread.Sleep(2000);
            }

            Console.ReadKey();
        }

        static void FuncStart()
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine("I am New Thread Loop");
            //    System.Threading.Thread.Sleep(2000);
            //}

            Form1 f = new Form1();
            //f.ShowDialog();
            
            if(f.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine("I am Windows Loop Going");
                    System.Threading.Thread.Sleep(2000);
                }

            }
        }
    }

    public class TimerExcuter
    {
        private System.Threading.Timer m_timer = null;

        public delegate void TimerHandle();
        public event TimerHandle TimerEvent;

        public TimerExcuter()
        {
        }

        public void Start()
        {
            m_timer = new System.Threading.Timer(Ecalplse, null, 2000, 1000);

        }

        public void Ecalplse(Object state)
        {
            if(TimerEvent != null)
            {
                foreach (TimerHandle d in TimerEvent.GetInvocationList())
                {
                    //异步调用
                    //传入委托作为一个状态对象
                    d.BeginInvoke(new AsyncCallback(ResultReturns), d);

                }

                //TimerEvent();
            }
        }

        //获取结果的回调方法
        public void ResultReturns(IAsyncResult iar)
        {
            //将状态对象转换为委托类型
            TimerHandle del = (TimerHandle)iar.AsyncState;
            //调用委托的EndInvoke方法获取结果
            //int result = del.EndInvoke(del);
            //显示结果
            //Console.WriteLine("Delegate returned result: {0}");
        }
    }

    public class DoEventOne
    {
        public DoEventOne()
        {

        }

        public void DoTimer()
        {
            //while(true)
            {
                Console.WriteLine("Do--1");
                //System.Threading.Thread.Sleep(8000);
            }
        }
    }

    public class DoEventTwo
    {
        public DoEventTwo()
        {

        }

        public void DoTimer()
        {
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("Do--2");
                //Debug.WriteLine("Do--2");
                //System.Threading.Thread.Sleep(8000);
            }
        }
    }

    public class DoEventThree
    {
        public DoEventThree()
        {

        }

        public void DoTimer()
        {
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine("Do--3");
                //Debug.WriteLine("Do--3");

                //System.Threading.Thread.Sleep(8000);
            }
        }
    }

}
