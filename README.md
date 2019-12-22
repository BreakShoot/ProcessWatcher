# ProcessWatcher
A non-elevated example of process watching

## Example Usage
```c#
class Program
{
    static void Main(string[] args)
    {
        ProcessWatcher processWatcher = new ProcessWatcher("procname");

        processWatcher.Created += (sender, proc) =>
        {
            Console.WriteLine("spotted!");
        };
    }
}
```
