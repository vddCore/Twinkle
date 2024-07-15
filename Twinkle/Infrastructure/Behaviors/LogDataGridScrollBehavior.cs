namespace Twinkle.Infrastructure.Behaviors;

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;
using PropertyChanged;
using Twinkle.API.Logging;

[DoNotNotify]
public class LogDataGridScrollBehavior : Behavior<DataGrid>
{
    private ObservableCollection<LogMessage> _messages = null!;
    
    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is not null)
        {
            _messages = (AssociatedObject.ItemsSource as ObservableCollection<LogMessage>)!;
            _messages.CollectionChanged += Observable_CollectionChanged;
            AssociatedObject?.ScrollIntoView(_messages.LastOrDefault(), null);
        }
    }

    private void Observable_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
        {
            AssociatedObject?.ScrollIntoView(_messages.LastOrDefault(), null);
        }
    }
}