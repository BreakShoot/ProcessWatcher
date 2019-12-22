class ProcessWatcer : IDisposable
{
    public event OnProcessCreatedDelegate Created;
    private readonly Timer _timer;
    private readonly string _processname;
    private bool _disposed = false;
    private Process _process;

    public ProcessWatcer(string processName)
    {
        _processname = processName;

        _timer = new Timer();
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    private void TimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        Process[] processes = Process.GetProcessesByName(this._processname);

        if (processes.Length == 1)
        {
            OnProcessCreated(processes[0]);
            _timer.Stop();
        }
    }

    protected virtual void OnProcessCreated(Process process)
    {
        this._process = process;
        this._process.EnableRaisingEvents = true;
        this._process.Exited += (self, e) => this._timer.Start();

        OnProcessCreatedDelegate handler = Created;

        if (handler != null)
        {
            handler(this, process);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _timer.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public delegate void OnProcessCreatedDelegate(Object sender, Process e);
}
