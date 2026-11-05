

using System.Globalization;

namespace AppDeMensagem.Shared.Components.Table.Templates.Models;


public enum CellAlign
{
    Center,
    Left,
    Right
}

public static class TableAlignExtensions
{
    public static string ToAlingText(this CellAlign aling) => aling switch
    {
        CellAlign.Left => "text-left",
        CellAlign.Center => "text-center",
        CellAlign.Right => "text-right",
        _ => "text-left"
    };

    public static string ToAlingMargin(this CellAlign aling) => aling switch
    {
        CellAlign.Left => "",
        CellAlign.Center => "mx-auto",
        CellAlign.Right => "ml-auto",
        _ => ""
    };

    public static string ToAlingJustify(this CellAlign aling) => aling switch
    {
        CellAlign.Left => "justify-left",
        CellAlign.Center => "justify-center",
        CellAlign.Right => "justify-right",
        _ => "justify-left"
    };
}
