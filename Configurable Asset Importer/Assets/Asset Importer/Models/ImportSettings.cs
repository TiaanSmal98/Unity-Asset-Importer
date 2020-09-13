using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ImportSettings
{
    public int ApplicationVersion = 0;
    public UniversalSettings UniversalSettings;
    public AndroidSettings AndroidSettings;
}
