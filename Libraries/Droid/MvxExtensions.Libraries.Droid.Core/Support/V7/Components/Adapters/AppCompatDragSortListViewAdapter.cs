using Android.Content;
using Android.Views;
using MvxExtensions.Libraries.Droid.Core.Support.V7.Components.Controls.DragSortListView;
using MvxExtensions.Libraries.Droid.Core.Support.V7.Components.Controls.DragSortListView.Interfaces;
using MvxExtensions.Libraries.Portable.Core.Extensions;
using MvvmCross.Binding.Droid.Views;
using System;
using System.Collections;

namespace MvxExtensions.Libraries.Droid.Core.Support.V7.Components.Adapters
{
    public class AppCompatDragSortListViewAdapter : MvxAdapter, IDropListener, IRemoveListener
    {
        #region Fields

        private readonly AppCompatDragSortListView _dslv;

        #endregion

        #region Properties

        public IList ItemsSourceList
        {
            get { return ItemsSource as IList; }
        }

        #endregion

        #region Constructor

        public AppCompatDragSortListViewAdapter(Context context, AppCompatDragSortListView dslv, int itemTemplateId)
            : base(context)
        {
            if (dslv == null)
                throw new ArgumentNullException("dslv");

            ItemTemplateId = itemTemplateId;
            _dslv = dslv;
        }

        #endregion

        #region Methods

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            DragSortItemView v;
            View child;

            if (convertView != null)
            {
                v = convertView as DragSortItemView;
                View oldChild = v.GetChildAt(0);
                child = base.GetView(position, oldChild, parent);
                if (child != oldChild)
                {
                    // shouldn't get here if user is reusing convertViews
                    // properly
                    if (oldChild != null)
                    {
                        v.RemoveViewAt(0);
                    }
                    v.AddView(child);
                }
            }
            else
            {
                v = base.GetView(position, null, parent) as DragSortItemView;
            }

            // Set the correct item height given drag state; passed
            // View needs to be measured if measurement is required.
            _dslv.AdjustItem(position + _dslv.HeaderViewsCount, v, true);

            return v;
        }

        protected override IMvxListItemView CreateBindableView(object dataContext, int templateId)
        {
            return new DragSortItemView(Context, BindingContext.LayoutInflaterHolder, dataContext, templateId);
        }

        public void Drop(int from, int to)
        {
            if (from != to && ItemsSourceList.SafeCount() > Math.Max(from, to))
            {
                var item = ItemsSourceList[from];
                ItemsSourceList.RemoveAt(from);
                ItemsSourceList.Insert(to, item);
                _dslv.UpdateItemsVisualState();
            }
        }

        public void Remove(int which)
        {
            if (ItemsSourceList.SafeCount() > which)
            {
                var item = ItemsSourceList[which];
                ItemsSourceList.Remove(item);
                _dslv.UpdateItemsVisualState();
            }
        }

        #endregion
    }
}