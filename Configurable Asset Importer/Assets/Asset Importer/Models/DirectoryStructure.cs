using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectoryStructure
{
    public DirectoryStructure()
    {
        this.settings = new ImportSettings();
        this.settings.UniversalSettings = new UniversalSettings();
        this.settings.AndroidSettings = new AndroidSettings();
    }

    public ImportSettings settings;
    public string path;

    public List<DirectoryStructure> childSettings;

    public override string ToString()
    {
        return path;
    }

}