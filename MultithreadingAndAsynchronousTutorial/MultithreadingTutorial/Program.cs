var counter = 0;
var dummy = new object();

var t1 = Task.Run(Increment);
var t2 = Task.Run(Increment);

Task.WaitAny(t1, t2);

Console.WriteLine($"The value of the counter is {counter}");


void Increment()
{
    for (int i = 0; i < 1_000_000_000; i++)
    {
        if (i > 0 && i % 100_000_000 == 0)
        {
            Console.WriteLine($"{i} was reached");
        }
        lock (dummy)
        {
            counter++;
        }
        //Interlocked.Increment(ref counter);
    }
    Console.WriteLine("The job is completed");
}
//var t1 = new Thread(LongRunningTask);
//t1.Name = "Thread 1";
//t1.IsBackground = true;
//
//var t2 = new Thread(LongRunningTask);
//t2.Name = "Thread 2";
//t2.IsBackground = true;
//
//t1.Start();
//t2.Start();
//Console.WriteLine("The worked thread has started");
//
//t1.Join();
//t2.Join();
//Console.WriteLine("The program ends");
//Console.ReadKey();
//
//void LongRunningTask()
//{
//    var list = new List<int>();
//
//    for (int i = 0; i < 1_000_000_000; i++)
//    {
//        if (i > 0 && i % 100_000_000 == 0)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}: {i} was reached");
//        }
//        list.Add(i);
//    }
//
//    Console.WriteLine($"{Thread.CurrentThread.Name}: The job is completed");
//}
//LongRunningTask();