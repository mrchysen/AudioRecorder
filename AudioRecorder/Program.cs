using NAudio.Wave;

var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
Directory.CreateDirectory(outputFolder);
var outputFilePath = Path.Combine(outputFolder, "recorded.wav");

using var waveIn = new WaveInEvent();
using var writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

waveIn.DataAvailable += (s, a) =>
{
    writer.Write(a.Buffer, 0, a.BytesRecorded);
    if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
    {
        waveIn.StopRecording();
    }
};

Console.WriteLine("Record is started");

waveIn.StartRecording();

await Task.Delay(5000);

waveIn.StopRecording();

Console.WriteLine("Record is over");
Console.WriteLine("Path:" + outputFilePath);