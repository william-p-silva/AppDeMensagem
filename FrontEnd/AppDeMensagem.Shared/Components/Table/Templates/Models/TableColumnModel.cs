

namespace AppDeMensagem.Shared.Components.Table.Templates.Models;

public sealed record TableColumnModel
{
    public string? Key { get; init; }
    public required string Label { get; init; }
    public CellAlign? Align { get; init; }

}
