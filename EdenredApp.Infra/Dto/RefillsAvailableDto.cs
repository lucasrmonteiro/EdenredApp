namespace EdenredApp.Infra.Dto;

public class RefillsAvailableDto
{
    public int[] Refills { get; private set; } = new[] { 5, 10, 20, 30, 50, 75, 100 };
}