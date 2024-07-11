using System.Text.Json.Serialization;
using EdenredApp.Infra.Dto;
using EdenredApp.Infra.Model;

namespace EdenredApp.API;

[JsonSerializable(typeof(CreditModel))]
[JsonSerializable(typeof(RefillsAvailableDto))]
public partial class AppJsonContext : JsonSerializerContext
{
}