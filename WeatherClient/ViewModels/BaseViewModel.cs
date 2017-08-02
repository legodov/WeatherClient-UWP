using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WeatherClient.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged(() => Title);
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void NotifyPropertyChanged(Expression<Func<object>> target)
        {
            if (target != null)
            {
                var body = target.Body as MemberExpression;
                if (body != null)
                {
                    NotifyPropertyChanged(body.Member.Name);
                }
            }
        }
    }
}
