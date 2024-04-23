using GM.Models;
using GM.ViewModels.Document;
using System.ComponentModel;

namespace GM.Commands;

public class SubmitDocumentDetailCommand : CommandBase
{
    private readonly AddDocumentViewModel _vm;
    private readonly User _user;
    public SubmitDocumentDetailCommand(AddDocumentViewModel vm, User user)
    {
        _vm = vm;
        _user = user;

        _vm.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_vm.DocumentNumber) && 
            !string.IsNullOrEmpty(_vm.SelectedDocumentName) &&
            base.CanExecute(parameter);
    }

    public override void Execute(object? parameter)
    {
        DocumentDetail documentDetail = new(
            new Document(_vm.SelectedDocumentName),
            _vm.DocumentNumber,
            _vm.IssuedDate,
            _vm.IssuedDate);


        _user.AddDocumentDetail(documentDetail);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AddDocumentViewModel.DocumentNumber) ||
            e.PropertyName == nameof(AddDocumentViewModel.SelectedDocumentName))
        {
            OnCanExecutedChanged();
        }
    }
}
