using E_Tracker.Infrastructure.StaticServices;

namespace E_Tracker.Infrastructure.Services.Storage;

     public class Storage
     {
         protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileReNameAsync(string pathOrContainerName, string fileName,HasFile hasFileMethod,  bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameService.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo = newFileName.IndexOf("-");
                    if (indexNo == -1)

                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";

                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo;
                            indexNo = newFileName.IndexOf("-", indexNo + 1);
                            if (indexNo == -1)
                            {
                                indexNo = lastIndex;
                                break;

                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo, indexNo2 - indexNo - 1);
                        int _fileNo = int.Parse(fileNo);
                        _fileNo++;
                        if (int.TryParse(fileNo, out _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo + 1, indexNo2 - indexNo - 1)
                                .Insert(indexNo, _fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";

                    }

                }

               // if (File.Exists($"{path}\\{newFileName}"))
               if (hasFileMethod(pathOrContainerName, newFileName))
               
                   return await FileReNameAsync(pathOrContainerName, newFileName, hasFileMethod,false);
                else
                    return newFileName;

            });
            return newFileName;

        }

    }

