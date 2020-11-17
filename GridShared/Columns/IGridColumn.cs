﻿using GridShared.Filtering;
using GridShared.Grouping;
using GridShared.OData;
using GridShared.Searching;
using GridShared.Sorting;
using GridShared.Totals;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GridShared.Columns
{
    public interface IGridColumn<T> : IGridColumn, IColumn<T>, ISortableColumn<T>,
        IFilterableColumn<T>, ISearchableColumn<T>, ITotalsColumn<T>, IGroupableColumn<T>
    {
        IGridCell GetValue(T instance);
        string GetFormatedValue(Func<T, string> expression, object value);
        (Type Type, object Value) GetTypeAndValue(T item);
        (bool IsSelectKey, Func<T,string> Expression, string Url, Func<IEnumerable<SelectItem>> SelectItemExpr, 
            Func<Task<IEnumerable<SelectItem>>> SelectItemExprAsync) IsSelectField { get; }
        IEnumerable<SelectItem> SelectItems { get; set; }
        InputType InputType { get; }
    }

    public interface IGridColumn : ISortableColumn, IFilterableColumn
    {
        Type ComponentType { get; }
        IList<Action<object>> Actions { get; }
        IList<Func<object, Task>> Functions { get; }
        object Object { get; }

        Type CreateComponentType { get; }
        Type UpdateComponentType { get; }
        Type ReadComponentType { get; }
        Type DeleteComponentType { get; }
        IList<Action<object>> CrudActions { get; }
        IList<Func<object, Task>> CrudFunctions { get; }
        object CrudObject { get; }
        bool EnableCard { get; }

        IGrid ParentGrid { get; }
        bool Hidden { get; set; }
        bool? ExcelHidden { get; set; }
        CrudHidden CrudHidden { get; }
        bool ReadOnlyOnUpdate { get; }
        bool IsPrimaryKey { get; }
        bool IsAutoGeneratedKey { get; }
        string TabGroup { get; }
        bool HeaderCheckbox { get; }
        bool SingleCheckbox { get; }
        string TooltipValue { get; }
        bool MultipleInput { get; }
    }

    /// <summary>
    ///     fluent interface for grid column
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IColumn<T>
    {
        /// <summary>
        ///     Set gridColumn title
        /// </summary>
        /// <param name="title">Title text</param>
        IGridColumn<T> Titled(string title);

        /// <summary>
        ///     Need to encode the content of the gridColumn
        /// </summary>
        /// <param name="encode">Yes/No</param>
        IGridColumn<T> Encoded(bool encode);

        /// <summary>
        ///     Sanitize column value from XSS attacks
        /// </summary>
        /// <param name="sanitize">If true values from this column will be sanitized</param>
        IGridColumn<T> Sanitized(bool sanitize);

        /// <summary>
        ///     Sets the width of the column
        /// </summary>
        IGridColumn<T> SetWidth(string width);

        /// <summary>
        ///     Sets the width of the column in pizels
        /// </summary>
        IGridColumn<T> SetWidth(int width);

        /// <summary>
        ///     Sets the width of the column on crud forms
        /// </summary>
        IGridColumn<T> SetCrudWidth(int width);

        /// <summary>
        ///     Sets the width of the column and its label on crud forms
        /// </summary>
        IGridColumn<T> SetCrudWidth(int width, int labelWidth);

        /// <summary>
        ///     Specify additional css class of the column
        /// </summary>
        IGridColumn<T> Css(string cssClasses);

        /// <summary>
        ///     Setup the custom classes for cells
        /// </summary>
        IGridColumn<T> SetCellCssClassesContraint(Func<T, string> contraint);

        /// <summary>
        ///     Specify the tab group for the column
        /// </summary>
        IGridColumn<T> SetTabGroup(string tabGroup);

        /// <summary>
        ///     Sets a column as checkbox with single selection
        /// </summary>
        IGridColumn<T> SetSingleCheckboxColumn();

        /// <summary>
        ///     Sets a column as checkbox
        /// </summary>
        IGridColumn<T> SetCheckboxColumn(bool headerCheckbox, Func<T, bool> expression);

        /// <summary>
        ///     Sets a column as checkbox
        /// </summary>
        IGridColumn<T> SetCheckboxColumn(bool headerCheckbox, Func<T, bool> expression, Func<T, bool> readonlyExpr);

        /// <summary>
        ///     Setup the custom rendere for property
        /// </summary>
        IGridColumn<T> RenderValueAs(Func<T, string> constraint);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Action<object>> actions);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Func<object,Task>> functions);
        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Action<object>> actions,
            IList<Func<object, Task>> functions);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Action<object>> actions, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Func<object, Task>> functions, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs(Type componentType, IList<Action<object>> actions, 
            IList<Func<object, Task>> functions, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>();

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Action<object>> actions);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Func<object, Task>> functions);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Action<object>> actions, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Func<object, Task>> functions, object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderComponentAs<TComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions, 
            object obj);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Action<object>> actions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Func<object, Task>> functions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Action<object>> actions, object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Func<object, Task>> functions, object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions,
            object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Action<object>> actions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Func<object, Task>> functions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Action<object>> actions, object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Func<object, Task>> functions, object obj, bool enableCard = true);

        /// <summary>
        ///     Setup the custom render for component
        /// </summary>
        IGridColumn<T> RenderCrudComponentAs<TCreateComponent, TReadComponent, TUpdateComponent, TDeleteComponent>(IList<Action<object>> actions, IList<Func<object, Task>> functions,
            object obj, bool enableCard = true);

        /// <summary>
        ///     Format column values with specified text pattern
        /// </summary>
        IGridColumn<T> Format(string pattern);

        /// <summary>
        ///     Calculate Sum of column values
        /// </summary>
        IGridColumn<T> Sum(bool enabled);

        /// <summary>
        ///     Calculate average of column values
        /// </summary>
        IGridColumn<T> Average(bool enabled);

        /// <summary>
        ///     Calculate max of column values
        /// </summary>
        IGridColumn<T> Max(bool enabled);

        /// <summary>
        ///     Calculate min of column values
        /// </summary>
        IGridColumn<T> Min(bool enabled);

        /// <summary>
        ///     Sets the column as hidden in crud views
        /// </summary>
        IGridColumn<T> SetCrudHidden(bool create, bool read, bool update, bool delete);

        /// <summary>
        ///     Sets the column as hidden in all crud views
        /// </summary>
        IGridColumn<T> SetCrudHidden(bool all);

        /// <summary>
        ///     Sets the column as hidden for excel export, 
        ///     overriding the default Hidden value (if not null)
        /// </summary>
        IGridColumn<T> SetExcelHidden(bool? all);

        /// <summary>
        ///     Sets the column as readonly on update.
        /// </summary>
        IGridColumn<T> SetReadOnlyOnUpdate(bool enabled);

        /// <summary>
        ///     Sets the column as primary key
        /// </summary>
        IGridColumn<T> SetPrimaryKey(bool enabled);

        /// <summary>
        ///     Sets the column as primary key and auto-generated 
        /// </summary>
        IGridColumn<T> SetPrimaryKey(bool enabled, bool autoGenerated);

        /// <summary>
        ///     Sets the column as select for CRUD components
        /// </summary>
        IGridColumn<T> SetSelectField(bool enabled, Func<T, string> expression, Func<IEnumerable<SelectItem>> selectItemExpr);

        /// <summary>
        ///     Sets the column as select for CRUD components
        /// </summary>
        IGridColumn<T> SetSelectField(bool enabled, Func<T, string> expression, Func<Task<IEnumerable<SelectItem>>> selectItemExprAsync);

        /// <summary>
        ///     Sets the column as select for CRUD components
        /// </summary>
        IGridColumn<T> SetSelectField(bool enabled, Func<T, string> expression, string url);

        /// <summary>
        ///     Sets the column input type for CRUD components
        /// </summary>
        IGridColumn<T> SetInputType(InputType inputType);

        /// <summary>
        ///     Sets the column input type for CRUD components
        /// </summary>
        IGridColumn<T> SetInputFileType(bool? multiple = null);

        /// <summary>
        ///    Allow grid to show a SubGrid
        /// </summary>
        IGridColumn<T> SubGrid(Func<object[], bool, bool, bool, bool, Task<IGrid>> subGrids, params (string,string)[] keys);

        /// <summary>
        ///    Allow grid to show a SubGrid
        /// </summary>
        IGridColumn<T> SubGrid(string tabGroup, Func<object[], bool, bool, bool, bool, Task<IGrid>> subGrids, params (string, string)[] keys);

        /// <summary>
        ///    Allow grid header to show a tooltip
        /// </summary>
        IGridColumn<T> SetTooltip(string value);
    }

    public interface IColumn
    {
        /// <summary>
        ///     Columns title
        /// </summary>
        string Title { get; }

        /// <summary>
        ///     Internal name of the gridColumn
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Internal name of the field name
        /// </summary>
        string FieldName { get; }

        /// <summary>
        ///     Width of the column
        /// </summary>
        string Width { get; set; }

        /// <summary>
        ///     Width of the column on crud forms
        /// </summary>
        int CrudWidth { get; set; }

        /// <summary>
        ///     Width of the column label on crud forms
        /// </summary>
        int CrudLabelWidth { get; set; }

        /// <summary>
        ///     EncodeEnabled
        /// </summary>
        bool EncodeEnabled { get; }

        bool SanitizeEnabled { get; }

        /// <summary>
        ///     Gets value of the gridColumn by instance
        /// </summary>
        /// <param name="instance">Instance of the item</param>
        IGridCell GetCell(object instance);

        /// <summary>
        ///     Gets gridColumn formated value
        /// </summary>
        /// <param name="instance">Instance of the item</param>
        string GetFormatedValue(object value);

        /// <summary>
        ///     Get custom css classes mapped to the cell
        /// </summary>
        string GetCellCssClasses(object item);
    }

    /// <summary>
    ///     fluent interface for grid sorted column
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISortableColumn<T> : IColumn
    {
        /// <summary>
        ///     List of column orderes
        /// </summary>
        IEnumerable<IColumnOrderer<T>> Orderers { get; }

        /// <summary>
        ///     Enable sort of the gridColumn
        /// </summary>
        /// <param name="sort">Yes/No</param>
        IGridColumn<T> Sortable(bool sort);

        /// <summary>
        ///     Setup the initial sorting direction of current column
        /// </summary>
        /// <param name="direction">Ascending / Descending</param>
        IGridColumn<T> SortInitialDirection(GridSortDirection direction);

        /// <summary>
        ///     Setup ThenBy sorting of current column
        /// </summary>
        IGridColumn<T> ThenSortBy<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        ///     Setup ThenBy sorting of current column
        /// </summary>
        IGridColumn<T> ThenSortBy<TKey>(Expression<Func<T, TKey>> expression, IComparer<TKey> comparer);

        /// <summary>
        ///     Setup ThenByDescending sorting of current column
        /// </summary>
        IGridColumn<T> ThenSortByDescending<TKey>(Expression<Func<T, TKey>> expression);

        /// <summary>
        ///     Setup ThenByDescending sorting of current column
        /// </summary>
        IGridColumn<T> ThenSortByDescending<TKey>(Expression<Func<T, TKey>> expression, IComparer<TKey> comparer);
    }

    public interface ISortableColumn : IColumn
    {
        /// <summary>
        ///     Enable sort for this column
        /// </summary>
        bool SortEnabled { get; }

        /// <summary>
        ///     Is current column sorted
        /// </summary>
        bool IsSorted { get; set; }

        /// <summary>
        ///     Sort direction of current column
        /// </summary>
        GridSortDirection? Direction { get; set; }
    }

    public interface IFilterableColumn<T>
    {
        /// <summary>
        ///     Collection of current column filter
        /// </summary>
        //IColumnFilter<T> Filter { get; }

        /// <summary>
        ///     Allows filtering for this column
        /// </summary>
        /// <param name="enalbe">Enable/disable filtering</param>
        IGridColumn<T> Filterable(bool enalbe);

        /// <summary>
        ///     Set up initial filter for this column
        /// </summary>
        /// <param name="type">Filter type</param>
        /// <param name="value">Filter value</param>
        IGridColumn<T> SetInitialFilter(GridFilterType type, string value);

        /// <summary>
        ///     Specify custom filter widget type for this column
        /// </summary>
        /// <param name="typeName">Widget type name</param>
        IGridColumn<T> SetFilterWidgetType(string typeName);

        /// <summary>
        ///     Specify custom filter widget type for this column
        /// </summary>
        /// <param name="typeName">Widget type name</param>
        /// <param name="widgetData">The data would be passed to the widget</param>
        IGridColumn<T> SetFilterWidgetType(string typeName, object widgetData);

        /// <summary>
        ///     Specify a list filter widget type for this column
        /// </summary>
        /// <param name="selectItems">List of selectable items</param>
        IGridColumn<T> SetListFilter(IEnumerable<SelectItem> selectItems, bool includeIsNull = false, bool includeIsNotNull = false);
    }

    public interface IFilterableColumn : IColumn
    {
        /// <summary>
        ///     Collection of current column filter
        /// </summary>
        IColumnFilter Filter { get; }

        /// <summary>
        ///     Internal name of the gridColumn
        /// </summary>
        bool FilterEnabled { get; }

        /// <summary>
        ///     Initial filter settings for the column
        /// </summary>
        ColumnFilterValue InitialFilterSettings { get; set; }

        string FilterWidgetTypeName { get; }

        object FilterWidgetData { get; }
    }

    public interface ISearchableColumn<T>
    {
        /// <summary>
        ///     Collection of current column search
        /// </summary>
        IColumnSearch<T> Search { get; }
    }

    public interface ITotalsColumn<T> : ITotalsColumn
    {
        /// <summary>
        ///     Collection of current column totals
        /// </summary>
        IColumnTotals<T> Totals { get; }
    }

    public interface ITotalsColumn
    {
        bool IsSumEnabled { get; set; }

        string SumString { get; set; }

        bool IsAverageEnabled { get; set; }

        string AverageString { get; set; }

        bool IsMaxEnabled { get; set; }

        string MaxString { get; set; }

        bool IsMinEnabled { get; set; }

        string MinString { get; set; }
    }

    public interface IExpandColumn<T>
    {
        /// <summary>
        ///     Collection of current column OData Expand
        /// </summary>
        IColumnExpand<T> Expand { get; }
    }

    public interface IGroupableColumn<T>
    {
        /// <summary>
        ///     Collection of current column groups
        /// </summary>
        IColumnGroup<T> Group { get; }
    }
}