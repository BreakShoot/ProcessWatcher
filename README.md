# ProcessWatcher
A non-elevated example of process watching

## Example Usage
```c#
class Program
{

    static void Main(string[] args)
    {
        ProcessWatcer processWatcher = new ProcessWatcer("procname");

        processWatcher.Created += (sender, proc) =>
        {
            Console.WriteLine("spotted!");
        };
    }
}```
