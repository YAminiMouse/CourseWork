using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL.AdditionalEntities
{
    public class WindowContext
    {
        private Dictionary<string, object> _resourses; 
        public object GetResourse(string key)
        {
            return _resourses[key];
        }
        public void SetResource(string key , object value)
        {
           if (_resourses.ContainsKey(key))
           {
                _resourses.Remove(key);
           }
            _resourses.Add(key, value);
        }
        public void SetCurrentWindow(Window window)
        {
            SetResource("CURRENT_WINDOW", window);
        }
        public Window GetCurrentWindow()
        {
            return (Window)GetResourse("CURRENT_WINDOW");
        }

        public void SetSubWindow(Window window)
        {
            var previousWindow = GetCurrentWindow();
            previousWindow.IsEnabled = false;
            SetCurrentWindow(window);
            SetPreviousWindow(previousWindow);
        }
        public void DropSubWindow()
        {
            var previousWindow = GetPreviousWindow();
            previousWindow.IsEnabled = true;
            SetCurrentWindow(previousWindow);
        }
        public void SetPreviousWindow(Window window)
        {
            SetResource("PREVIOUS_WINDOW", window);
        }

        public Window GetPreviousWindow()
        {
            return (Window)GetResourse("PREVIOUS_WINDOW");
        }
        public WindowContext() 
        {
            _resourses = new Dictionary<string, object>();
        }
    }
}
