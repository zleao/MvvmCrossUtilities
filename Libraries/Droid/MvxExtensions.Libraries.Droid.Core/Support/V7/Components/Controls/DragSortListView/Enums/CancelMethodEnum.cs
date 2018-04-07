namespace MvxExtensions.Libraries.Droid.Core.Support.V7.Components.Controls.DragSortListView.Enums
{
    /// <summary>
    /// Enum telling where to cancel the ListView action when a
    /// drag-sort begins
    /// </summary>
    public enum CancelMethodEnum
    {
        NoCancel = 0,
        OnTouchEvent = 1,
        OnInterceptTouchEvent = 2,
    }
}