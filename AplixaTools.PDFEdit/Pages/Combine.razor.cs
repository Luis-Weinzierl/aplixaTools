﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using AplixaTools.PDFEdit.Components;
using AplixaTools.PDFEdit.Services;
using AplixaTools.PDFEdit.Shared;
using AplixaTools.Shared.Components;
using iText.Kernel.Pdf;
using AplixaTools.PDFEdit.Models;

namespace AplixaTools.PDFEdit.Pages;

[Route(Routes.CombineTool)]
public partial class Combine : IDisposable
{
    [Inject] public PdfMutationQueueService MutationService { get; set; }
    [Inject] public JsInteropService JsInterop { get; set; }

    public Modal ConfirmClearModal { get; set; }
    public PageSettingsModal PageSettingsModal { get; set; }
    public OutputPages OutputPages { get; set; }
    public InputPages InputPages { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            JsInterop.Startup();
            StateHasChanged();
        }
        base.OnAfterRender(firstRender);
    }

    private void ConfirmClearOnClick()
    {
        OutputPages.StartLoading();
        MutationService.QuequeMutation(new PdfClearMutation());
        ConfirmClearModal.Hide();
    }

    private void PageSettingsOnSave()
    {
        OutputPages.StartLoading();
        MutationService.QuequeMutation(
            new PdfTransformPageMutation(
                OutputPages.SelectedPage,
                0,
                new PdfTransform
                {
                    Angle = PageSettingsModal.Rotation,
                }
            )
        );
    }

    private void UpdateMerge()
    {
        if (OutputPages is not {})
        {
            return;
        }
        OutputPages.StartLoading();
        StateHasChanged();

        StateHasChanged();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        OutputPages.Dispose();
    }
}
