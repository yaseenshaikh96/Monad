public class Program
{
    public static void Main()
    {
        int a = 5;
        thingsWithLog<int> b = Convert(a);
        thingsWithLog<int> c = Run(b, MyAdd);
        thingsWithLog<int> d = Run(c, MyMulti);
        thingsWithLog<int> e = Run(d, MyAdd);
        thingsWithLog<int> f = Run(e, MyMulti);

        //foreach (string log in f.logs)
        //  System.Console.WriteLine(log);


        int g = 1;
        thingsWithLog<int> h = Convert(g);
        thingsWithLog<int> i = Run(Run(Run(Run(Run(Run(h, MyAdd), MyMulti), MyAdd), MyMulti), MyAdd), MyAdd);

        foreach (string log in i.logs)
            System.Console.WriteLine(log);
    }

    public static thingsWithLog<int> MyAdd(int a)
    {
        List<string> log = new List<string>();
        log.Add("Added: " + a + " => " + (a + a));
        return new thingsWithLog<int>(a + a, log);
    }

    public static thingsWithLog<int> MyMulti(int a)
    {
        List<string> log = new List<string>();
        log.Add("Multi: " + a + " => " + (a * a));
        return new thingsWithLog<int>(a * a, log);
    }

    public delegate thingsWithLog<T> FunctionType<T>(T data);
    public class thingsWithLog<T>
    {
        public T data;
        public List<string> logs;
        public thingsWithLog(T data, List<string> logs)
        {
            this.data = data;
            this.logs = logs;
        }
    }

    public static thingsWithLog<T> Convert<T>(T data) => new thingsWithLog<T>(data, new List<string>());

    public static thingsWithLog<T> Run<T>(thingsWithLog<T> data, FunctionType<T> functionToExecute)
    {
        thingsWithLog<T> newData = functionToExecute(data.data);
        return new thingsWithLog<T>(newData.data, data.logs.Concat(newData.logs).ToList());
    }
}