using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indego.Services;
using Microsoft.AspNetCore.Components;

namespace Indego;
public class BasePage : ComponentBase
{
    [Inject]
    protected GlobalState GlobalState { get; set; }

    [Inject]
    protected Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

    [Inject]
    protected IIndegoService IndegoService { get; set; }

    public I18nText.Strings I18nStrings { get; set; }

    protected override async Task OnInitializedAsync()
    {
        I18nStrings = await I18nText.GetTextTableAsync<I18nText.Strings>(this);
        GlobalState.OnChange += StateHasChanged;
    }
    public void Dispose()
    {
        GlobalState.OnChange -= StateHasChanged;
    }
}
