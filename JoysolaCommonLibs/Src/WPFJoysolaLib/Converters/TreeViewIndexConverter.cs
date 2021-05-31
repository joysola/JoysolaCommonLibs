using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFJoysolaLib.Converters
{
    /// <summary>
    /// 树形控件index
    /// </summary>
    public class TreeViewIndexConverter : IMultiValueConverter, IValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is TreeView tree && values[1] is TreeViewItem item)
            {
                var result = GetIndexfromTree(tree, item, 0);
                return $"{result}.";
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从树中找对应treeitem的index
        /// </summary>
        /// <param name="tree">树</param>
        /// <param name="treeItem">要找的item</param>
        /// <param name="index">初始索引</param>
        /// <returns></returns>
        private int GetIndexfromTree(TreeView tree, TreeViewItem treeItem, int index)
        {
            foreach (var item in tree.Items)
            {
                index++;
                var curTreeItem = tree.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (curTreeItem == item)
                {
                    break;
                }
                var success = GetIndex(curTreeItem, treeItem, ref index);
                if (success) // 成功找到
                {
                    break;
                }
            }
            return index;
        }
        /// <summary>
        /// 寻找当前curTreeItem里是否包含treeItem
        /// </summary>
        /// <param name="curTreeItem">当前item</param>
        /// <param name="treeItem">需要寻找的item</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        private bool GetIndex(TreeViewItem curTreeItem, TreeViewItem treeItem, ref int index)
        {
            if (curTreeItem == treeItem) // 直接找到
            {
                return true;
            }
            foreach (var item in curTreeItem.Items)
            {
                index++;
                var targetItem = curTreeItem.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (targetItem != null && treeItem == targetItem)
                {
                    return true;
                }
                else // 未找到
                {
                    GetIndex(targetItem, treeItem, ref index);
                }
            }
            return false;
        }
    }
}
