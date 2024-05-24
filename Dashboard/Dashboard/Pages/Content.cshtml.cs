using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Web;

namespace Dashboard.Pages
{


    public class ContentModel : PageModel
    {
        [BindProperty]
        public string SearchString { get; set; } = "";

        [BindProperty]
        public string SectionFilter { get; set; } = "";

        public static List<Section> content = new List<Section>();

        public static Dictionary<int, string> sections = new Dictionary<int, string>();

        public static List<string> sectionTitles = new List<string>();

       public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Nombre")))
            {
                return RedirectToPage("./Login");
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Utils", "Data", "content.json");
            Console.WriteLine(filePath);
            sections.Clear();
            sectionTitles.Clear();
            
            try
            {
                string json = System.IO.File.ReadAllText(filePath);
                content = JsonSerializer.Deserialize<List<Section>>(json);

                // Sort content in each section alphabetically

                for (int i = 0; i < content.Count; i++)
                {
                Section section = content[i];
                section.concepts = section.concepts.OrderBy(x => x.title).ToList();
                sections.Add(i, section.section);
                sectionTitles.Add(section.section);
                // sections.Add(new Ids { id = i, title = section.section });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // Check if the search query is present
            if (Request.Query.ContainsKey("search"))
            {
                SearchString = Request.Query["search"].ToString() ?? "";
                SearchString = HttpUtility.UrlDecode(SearchString);
                List<Section> newContent = new List<Section>();
                Console.WriteLine("SEARCH: " + SearchString);    

                // Set the new content to concepts that contain the search string
                for (int i = 0; i < content.Count; i++)
                {
                    Section section = content[i];
                    List<Concept> newConcepts = new List<Concept>();
                    for (int j = 0; j < section.concepts.Count; j++)
                    {
                        Concept concept = section.concepts[j];
                        if (concept.title.ToLower().Contains(Request.Query["search"].ToString().ToLower()))
                        {
                            newConcepts.Add(concept);
                        }
                    }
                    if (newConcepts.Count > 0)
                    {
                        section.concepts = newConcepts;
                        newContent.Add(section);
                    }
                }

                content = newContent;
                Console.WriteLine(content.Count);
            }

            if (Request.Query.ContainsKey("filter"))
            {
                if (Request.Query["filter"] != "")
                {
                    SectionFilter = HttpUtility.UrlDecode(SectionFilter);
                    SectionFilter = Request.Query["filter"].ToString();
                    content = content.Where(s => s.section.ToLower().Contains(SectionFilter.ToLower())).ToList();
                }
            }

            return Page();
        }

        public void OnPost()
        {
            string path = $"Content?";
            
            if (Request.Form.ContainsKey("reset"))
            {
                Response.Redirect(path);
                return;
            }


            if (!String.IsNullOrEmpty(SearchString) && SearchString != "")
            {
                string encodedSearch = HttpUtility.UrlEncode(SearchString);
                path += $"&search={encodedSearch}";
            }

            if (SectionFilter != "")
            {
                string encodedFilter = HttpUtility.UrlEncode(SectionFilter);
                path += $"&filter={encodedFilter}";
            }
            
            Console.WriteLine(path);
            Response.Redirect(path);
        }
    }

}
