using System.Text.Json.Serialization;
using MobileCredits.Infra.Dto;
using MobileCredits.Infra.Model;

namespace MobileCredits.API;

[JsonSerializable(typeof(CreditModel))]
[JsonSerializable(typeof(RefillsAvailableDto))]
public partial class AppJsonContext : JsonSerializerContext
{
}