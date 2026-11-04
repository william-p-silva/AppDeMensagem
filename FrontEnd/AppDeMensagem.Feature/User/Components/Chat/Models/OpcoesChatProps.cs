using Microsoft.AspNetCore.Components;

namespace AppDeMensagem.Feature.User.Components.Chat.Models;

public sealed record OpcoesChatProps(
	string Text,
	Type Icone,
	int IconSize = 16,
	int IconStroke = 2,
    EventCallback OnClick = default
);
