using System.Security.Cryptography.X509Certificates;
using Code.Configs;

namespace Code.UI
{
    public interface IUiView
    {
        ViewNames Name { get; }
        void Open();
        void  Close();
    }
}