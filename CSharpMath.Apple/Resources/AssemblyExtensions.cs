﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpMath {
  internal static class AssemblyExtensions {
    public static string ShortName(this Assembly This) {
      string fullName = This.FullName;
      string r = fullName.Split(",".ToCharArray()).First();
      return r;
    }
    public static string ManifestResourcePrefix(this Assembly assembly) {
      string r = null;
      string resourceString = ".Resources.";
      int resourcesStringLength = resourceString.Length;
      string[] names = assembly.GetManifestResourceNames();
      foreach (string name in names) {
        int resIndex = name.LastIndexOf(resourceString, StringComparison.OrdinalIgnoreCase);
        if (resIndex > 0) {
          int resEndIndex = resIndex + resourcesStringLength;
          r = name.Substring(0, resEndIndex);
          break;
        }
      }
      return r;
    }
    public static string AssemblyPath(this Assembly assembly) {
      Module m = assembly.ManifestModule;
      ;
      string r = m?.FullyQualifiedName;
      return r;
    }
    public static List<string> ManifestEntriesWithPrefix(this Assembly assembly, string prefix
#if DEBUG
      , bool recursingIsOK = true
#endif
    ) {
      List<string> r = new List<string>();
      string namePrefix = assembly.ManifestResourcePrefix();
      if (namePrefix != null) {
        int resEndIndex = namePrefix.Length;
        string[] names = assembly.GetManifestResourceNames();
        string fullPrefix = namePrefix + prefix;
        foreach (string name in names) {
          if (name.StartsWith(fullPrefix, StringComparison.OrdinalIgnoreCase)) {
            string removeMyName = name.Substring(resEndIndex);
            r.Add(removeMyName);
          }
        }
#if DEBUG
        if (r.Count == 0) {
          string errorMessage;
          if (fullPrefix.ToLowerInvariant().IndexOf("resources.resources.resources", StringComparison.OrdinalIgnoreCase) != -1) {
            errorMessage = prefix + " not found!  Input path should not start with 'Resources' as that is added in this method.";
          } else {
            errorMessage = prefix + " not found!  Probably either an incorrect path or incorrect resource type (should be EmbeddedResource).";
          }

          if (recursingIsOK) {
            int length = prefix.Length;
            while (length > 1) {
              length--;
              string shorterPrefix = prefix.Substring(0, length);
              List<string> shorterList = assembly.ManifestEntriesWithPrefix(shorterPrefix, false);
              if (shorterList.Count > 1) {
                string firstPartial = shorterList[0];
              }
            }
            if (length == 1) {
              throw new IOException("No matches found, even of the first character");
            }
          }
        }
#endif
      }
      return r;
    }
  }
}