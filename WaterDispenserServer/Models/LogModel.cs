namespace WaterDispenserServer.Models;

public class LogModel
{
    public int Temperature { get; set; }
    public int Water_Dispensed { get; set; }
    public int Filter_Wear { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
}