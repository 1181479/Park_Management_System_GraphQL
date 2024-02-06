using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.LicensePlateReader.Core.IServices
{
    public interface ILicensePlateReaderService
    {
        bool UpdateLicensePlate(string newLicensePlate);
        Task<bool> ReadLicensePlateEntrance();

    }
}
