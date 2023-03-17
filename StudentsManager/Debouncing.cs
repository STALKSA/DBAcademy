using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager
{
    public class DebounceDispatcher
    {
        private static int _textVersion = 0;
        public static async Task Debounce(int timeMilSeconds, Func<Task> func)
        {
            _textVersion++;
            var text = _textVersion;

            await Task.Delay(timeMilSeconds);

            if (text == _textVersion)
            {
                await func();
            }
           
            
    }
}
}
