using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace TP2_RazorPages.Pages.Files
{
    public class SaveNoteModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public SaveNoteModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "Digite uma anotação.")]
            public string Content { get; set; } = string.Empty;
        }

        public string? SavedFileName { get; set; }

        public string? SelectedFileName { get; set; }

        public string? SelectedContent { get; set; }

        public List<string> Files { get; set; } = new();

        private string GetFilesDirectory()
        {
            string directory = Path.Combine(
                _environment.WebRootPath, "files");

            Directory.CreateDirectory(directory);

            return directory;
        }

        private void LoadFiles()
        {
            Files = Directory
                .GetFiles(GetFilesDirectory(), "*.txt")
                .Select(file => Path.GetFileName(file))
                .OrderByDescending(file => file)
                .ToList();
        }

        public void OnGet()
        {
            LoadFiles();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadFiles();
                return Page();
            }

            SavedFileName =
                $"note-{DateTime.Now:yyyyMMddHHmmssfff}.txt";

            string filePath = Path.Combine(
                GetFilesDirectory(), SavedFileName);
            await System.IO.File.WriteAllTextAsync(
        filePath, Input.Content);
            LoadFiles();

            return Page();
        }

        private bool IsValidFile(string? file)
        {
            return !string.IsNullOrWhiteSpace(file)
                && Path.GetFileName(file) == file
                && string.Equals(
                    Path.GetExtension(file),
                    ".txt",
                    StringComparison.OrdinalIgnoreCase);
        }

        public async Task<IActionResult> OnGetViewAsync(string file)
        {
            if (!IsValidFile(file))
            {
                return BadRequest();
            }

            string filePath = Path.Combine(
                GetFilesDirectory(), file);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            SelectedFileName = file;

            SelectedContent =
                await System.IO.File.ReadAllTextAsync(filePath);

            LoadFiles();

            return Page();
        }

        public IActionResult OnGetDownload(string file)
        {
            if (!IsValidFile(file))
            {
                return BadRequest();
            }

            string filePath = Path.Combine(
                GetFilesDirectory(), file);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "text/plain", file);
        }
    }
}