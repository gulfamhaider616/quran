using System.Collections.Generic;
using System.Text;

namespace Quran.Helpers
{
    // Builds SEO-friendly URL slugs from names (e.g. "Al-Faatiha" -> "al-faatiha") and
    // resolves a slug back to a record by matching against a candidate list. Used to keep
    // primary-key IDs out of public/admin URLs.
    public static class SlugHelper
    {
        public static string Make(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return string.Empty; }

            StringBuilder sb = new StringBuilder(text.Length);
            bool pendingDash = false;
            foreach (char c in text.Trim())
            {
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    if (pendingDash && sb.Length > 0) { sb.Append('-'); }
                    pendingDash = false;
                    sb.Append(c);
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    if (pendingDash && sb.Length > 0) { sb.Append('-'); }
                    pendingDash = false;
                    sb.Append((char)(c + 32));
                }
                else
                {
                    // Any separator/punctuation/non-ASCII collapses to a single dash boundary.
                    pendingDash = true;
                }
            }
            return sb.ToString();
        }

        // Returns the index of the first item whose slugified key equals the given slug, else -1.
        public static int IndexOfSlug<T>(IEnumerable<T> items, System.Func<T, string> keySelector, string slug)
        {
            if (string.IsNullOrWhiteSpace(slug)) { return -1; }
            string target = slug.Trim().ToLowerInvariant();
            int i = 0;
            foreach (T item in items)
            {
                if (Make(keySelector(item)) == target) { return i; }
                i++;
            }
            return -1;
        }
    }
}
