using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Landing.Win.UI
{
    public class ListBoxExtenders : DependencyObject
    {
        public static readonly DependencyProperty AutoScrollToEndProperty = DependencyProperty.RegisterAttached("AutoScrollToEnd", typeof(bool), typeof(ListBoxExtenders), new UIPropertyMetadata(default(bool), OnAutoScrollToEndChanged));

        /// <summary>
        /// Возвращает значение AutoScrollToEndProperty
        /// </summary>
        /// <param name="obj">Объект зависимости, значение которого должно быть возвращено</param>
        /// <returns></returns>
        public static bool GetAutoScrollToEnd(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToEndProperty);
        }

        /// <summary>
        /// Устанавливает значение AutoScrollToEndProperty
        /// </summary>
        /// <param name="obj">Объект зависимости, значение которого должно быть установлено</param>
        /// <param name="value">Значение, которое должно быть присвоено AutoScrollToEndProperty</param>
        public static void SetAutoScrollToEnd(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToEndProperty, value);
        }

        /// <summary>
        /// Этот метод будет вызываться, когда свойство AutoScrollToEnd
        ///  было изменено
        /// </summary>
        /// <param name="s">Отправитель(ListBox)</param>
        /// <param name="e">Дополнительная информация</param>
        public static void OnAutoScrollToEndChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var listBox = s as ListBox;
            var listBoxItems = listBox.Items;
            var data = listBoxItems.SourceCollection as INotifyCollectionChanged;

            var scrollToEndHandler = new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                (s1, e1) =>
                {
                    if (listBox.Items.Count > 0)
                    {
                        object lastItem = listBox.Items[listBox.Items.Count - 1];
                        listBoxItems.MoveCurrentTo(lastItem);
                        listBox.ScrollIntoView(lastItem);
                    }
                });

            if ((bool)e.NewValue)
                data.CollectionChanged += scrollToEndHandler;
            else
                data.CollectionChanged -= scrollToEndHandler;
        }
    }
}
