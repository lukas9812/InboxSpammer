using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using SuperSpammer.Common;
using SuperSpammer.Infastructure;
using SuperSpammer.Storage.Collections;
using SuperSpammer.Storage.Infrastructure;

namespace SuperSpammer.WebApi.Pages;

public class SenderSetup : PageModel
{
    public SenderSetup(ILoggerFactory loggerFactory, IMemoryCache cache,
        ISenderRepository senderRepository)
    {
        _logger = loggerFactory.CreateLogger<SenderSetup>();
        _cache = cache;
        _senderRepository = senderRepository;
        
    }
    
    [BindProperty]
    [Required]
    [EmailAddress]
    public string SenderEmail { get; set; }
    
    [BindProperty]
    [Required]
    public string SenderEmailPassword { get; set; }
    
    public string Message { get; set; }

    public void OnPost()
    {
        var sender = new SenderDto()
        {
            Email = SenderEmail,
            Password = SenderEmailPassword,
            Name = "",
            Id = ""
        };

        _senderRepository.Create(sender);
        
        // _cache.Set("UserEmail", SenderEmail, TimeSpan.FromMinutes(15));
        // _cache.Set("UserEmailPwd", SenderEmail, TimeSpan.FromMinutes(15));
        _logger.LogInformation("Credentials were set to the cache memory.");
    }
    public void OnGet()
    {
        SenderEmail = _cache.Get<string>("UserEmail") ?? string.Empty;
        SenderEmailPassword = _cache.Get<string>("UserEmailPwd") ?? string.Empty;
    }
    
    readonly IMemoryCache _cache;
    readonly ILogger<SenderSetup> _logger;
    readonly ISenderRepository _senderRepository;
}