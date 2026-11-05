using Microsoft.AspNetCore.Components;
 

namespace AppDeMensagem.Shared.Components.Table.Templates.Models;


public abstract record TableCellModel
{
    public CellAlign Align { get; set; }
}

public sealed record TitleCell : TableCellModel
{
    public required string Title { get; init; } = string.Empty;
    public string? SubTitle { get; init; }
    public string? MaxWidth { get; init; } = "max-w-[140px]";
    public string? ClassName { get; init; }
}

public sealed record TextCell : TableCellModel
{
    public required string Text { get; init; } = string.Empty;
    public string? MaxWidth { get; init; } = "max-w-[140px]";
    public string? ClassName { get; init; }
}

public sealed record BadgeCell : TableCellModel
{
    public required string Label { get; init; }
    public required string Color { get; init; }
    public RenderFragment? Icon { get; init; }
    public string? MaxWidth { get; init; } = "max-w-[140px]";
    public string? ClassName { get; init; }
}

public sealed record CustomCell : TableCellModel
{
    public required RenderFragment Content { get; init; }
    public string? ClassName { get; init; }
}