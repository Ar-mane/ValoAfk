using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ValoAfk.controllers
{
    public class Worker
    {
        public bool IsRunning { get; private set; }

        private Worker()
        {
        }

        internal static Worker CreateInstance()
        {
            return new Worker();
        }

        private CancellationTokenSource _tokenSource;


        private async Task Job(Config currentConfig)
        {
            var characters = Encoding.ASCII.GetBytes(currentConfig.Pattern.ToUpper());
            while (true)
            {
                await Task.Delay(currentConfig.Timeout, _tokenSource.Token);
                Array.ForEach(characters, Windows.PressButton);
            }
        }

        public void Start(Config config)
        {
            IsRunning = true;
            _tokenSource = new CancellationTokenSource();
            Task.Run(() => Job(config), _tokenSource.Token);
        }

        public void Stop()
        {
            IsRunning = false;
            _tokenSource.Cancel();
        }
    }
}