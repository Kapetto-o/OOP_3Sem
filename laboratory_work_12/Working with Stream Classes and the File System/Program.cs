using System;
using System.IO;
using System.IO.Compression;

public class LPALog
{
    private string logFilePath = "LPAlogfile.txt";

    public void WriteLog(string action, string details)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {action} - {details}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
        }
    }

    public void ReadLog()
    {
        try
        {
            using (StreamReader reader = new StreamReader(logFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения лога: {ex.Message}");
        }
    }

    public void SearchLog(string keyword)
    {
        try
        {
            using (StreamReader reader = new StreamReader(logFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(keyword))
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка поиска в логе: {ex.Message}");
        }
    }
}


public class LPADiskInfo
{
    public void DisplayDiskInfo()
    {
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"Имя: {drive.Name}");
                Console.WriteLine($"Объем: {drive.TotalSize} байт");
                Console.WriteLine($"Доступный объем: {drive.AvailableFreeSpace} байт");
                Console.WriteLine($"Файловая система: {drive.DriveFormat}");
                Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                Console.WriteLine();
            }
        }
    }
}


public class LPAFileInfo
{
    public void DisplayFileInfo(string filePath)
    {
        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                Console.WriteLine($"Полный путь: {fileInfo.FullName}");
                Console.WriteLine($"Размер: {fileInfo.Length} байт");
                Console.WriteLine($"Расширение: {fileInfo.Extension}");
                Console.WriteLine($"Имя: {fileInfo.Name}");
                Console.WriteLine($"Дата создания: {fileInfo.CreationTime}");
                Console.WriteLine($"Дата изменения: {fileInfo.LastWriteTime}");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения информации о файле: {ex.Message}");
        }
    }
}


public class LPADirInfo
{
    public void DisplayDirInfo(string dirPath)
    {
        try
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            if (dirInfo.Exists)
            {
                Console.WriteLine($"Количество файлов: {dirInfo.GetFiles().Length}");
                Console.WriteLine($"Время создания: {dirInfo.CreationTime}");
                Console.WriteLine($"Количество поддиректориев: {dirInfo.GetDirectories().Length}");
                Console.WriteLine($"Родительская директория: {dirInfo.Parent?.FullName}");
            }
            else
            {
                Console.WriteLine("Директория не найдена.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения информации о директории: {ex.Message}");
        }
    }
}


public class LPAFileManager
{
    public void ManageFiles(string sourceDir, string fileExtension)
    {
        try
        {
            // 1. Чтение списка файлов и папок заданного диска

            string[] directories = Directory.GetDirectories(sourceDir);

            // 2. Создание директории LPAInspect
            string inspectDir = Path.Combine(sourceDir, "LPAInspect");
            Directory.CreateDirectory(inspectDir);

            // 3. Создание текстового файла LPAdirinfo.txt и сохранение информации о файлах
            string dirInfoFile = Path.Combine(inspectDir, "LPAdirinfo.txt");

            string[] files = Directory.GetFiles(inspectDir);
            using (StreamWriter writer = new StreamWriter(dirInfoFile))
            {
                writer.WriteLine("Список файлов:");
                foreach (var file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    writer.WriteLine($"{fileInfo.Name} - {fileInfo.FullName}");
                }
                foreach (var directory in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    writer.WriteLine($"Директория: {dirInfo.Name} - {dirInfo.FullName}");
                }
            }

            // 4. Создание копии файла и переименование его
            if (files.Length > 0)
            {
                string fileToCopy = files[0]; // Берем первый файл для копирования
                string copyFilePath = Path.Combine(inspectDir, "Copy_" + Path.GetFileName(fileToCopy));
                File.Copy(fileToCopy, copyFilePath);
                Console.WriteLine($"Создана копия файла: {copyFilePath}");

                // Удаляем оригинальный файл
                File.Delete(fileToCopy);
                Console.WriteLine($"Удален оригинальный файл: {fileToCopy}");
            }

            string[] newfiles = Directory.GetFiles(inspectDir);
            using (StreamWriter writer = new StreamWriter(dirInfoFile))
            {
                writer.WriteLine("Список файлов:");
                foreach (var file in newfiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    writer.WriteLine($"{fileInfo.Name} - {fileInfo.FullName}");
                }
                foreach (var directory in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    writer.WriteLine($"Директория: {dirInfo.Name} - {dirInfo.FullName}");
                }
            }

            // 5. Создать еще один директорий LPAFiles
            string filesDir = Path.Combine(sourceDir, "LPAFiles");
            Directory.CreateDirectory(filesDir);

            // 6. Скопировать файлы с заданным расширением
            foreach (var file in newfiles)
            {
                if (Path.GetExtension(file).Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    string destPath = Path.Combine(filesDir, Path.GetFileName(file));
                    File.Copy(file, destPath);
                    Console.WriteLine($"Скопирован файл: {destPath}");
                }
            }

            // 7. Переместить LPAFiles в LPAInspect
            string newFilesDir = Path.Combine(inspectDir, "LPAInspect");
            Directory.Move(filesDir, newFilesDir);
            Console.WriteLine($"Директорий LPAFiles перемещен в LPAInspect.");

            // 8. Архивировать файлы
            string zipPath = Path.Combine(inspectDir, "LPAFiles.zip");
            ZipFile.CreateFromDirectory(newFilesDir, zipPath);
            Console.WriteLine($"Создан архив: {zipPath}");

            // 9. Разархивировать его в другой директории
            string extractedDir = Path.Combine(inspectDir, "ExtractedFiles");
            ZipFile.ExtractToDirectory(zipPath, extractedDir);
            Console.WriteLine($"Архив разархивирован в директорию: {extractedDir}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        string directoryPath = @"D:\GitHub\BSTU\course_2\OOP_3\laboratory_work_9\LabTest";
        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Директория не найдена: {directoryPath}");

            try
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Создана директория: {directoryPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка создания директории: {ex.Message}");
            }
        }
        else
        {

            LPAFileManager fileManager = new LPAFileManager();
            fileManager.ManageFiles(directoryPath, ".txt");

            LPALog logger = new LPALog();
            logger.WriteLog("Запуск программы", "Программа была запущена.");


            LPADiskInfo diskInfo = new LPADiskInfo();
            Console.WriteLine("Информация о дисках:");
            diskInfo.DisplayDiskInfo();

            LPAFileInfo fileInfo = new LPAFileInfo();
            string filePath = "D:\\GitHub\\BSTU\\course_2\\OOP_3\\laboratory_work_9\\LabTest\\LPAProgram.txt";
            Console.WriteLine($"\nИнформация о файле: {filePath}");
            fileInfo.DisplayFileInfo(filePath);


            LPADirInfo dirInfo = new LPADirInfo();
            string dirPath = "D:\\GitHub\\BSTU\\course_2\\OOP_3\\laboratory_work_9\\LabTest";
            Console.WriteLine($"\nИнформация о директории: {dirPath}");
            dirInfo.DisplayDirInfo(dirPath);


            Console.WriteLine("\nСодержимое лога:");
            logger.ReadLog();


            string keyword = "Запуск";
            Console.WriteLine($"\nРезультаты поиска по ключевому слову '{keyword}':");
            logger.SearchLog(keyword);
        }
    }
}