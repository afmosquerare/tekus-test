using System.Reflection;

namespace Application;

public class PresentationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof( PresentationAssemblyReference ).Assembly;
}