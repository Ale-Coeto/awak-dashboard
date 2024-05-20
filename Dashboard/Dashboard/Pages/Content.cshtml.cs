using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Dashboard.Pages
{
    public class ContentModel : PageModel
    {
        [BindProperty]
        public string SearchString { get; set; } = "";

        [BindProperty]
        public string SectionFilter { get; set; } = "";

        public static List<Section> content = new List<Section>();
        public void OnGet()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Utils", "Data", "content.json");

            try
            {
                string json = System.IO.File.ReadAllText(filePath);
                content = JsonSerializer.Deserialize<List<Section>>(json);

                // Sort content in each section alphabetically
                foreach (Section section in content)
                {
                    section.concepts = section.concepts.OrderBy(x => x.title).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (Request.Query.ContainsKey("search"))
            {
                SearchString = Request.Query["search"].ToString() ?? "";
                List<Section> newContent = new List<Section>();

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
                SectionFilter = Request.Query["filter"].ToString() ?? "";
                content = content.Where(s => s.section.ToLower().Contains(SectionFilter.ToLower())).ToList();
            }


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
                path += $"&search={SearchString}";
            }

            if (!String.IsNullOrEmpty(SectionFilter) && SectionFilter != "")
            {
                path += $"&filter={SectionFilter}";
            }

            Response.Redirect(path);
        }

    }


}
