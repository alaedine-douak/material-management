using GM.Models;
using GM.ViewModels.Document;

namespace GM.Commands;

public class SubmitDocumentDetailCommand : CommandBase
{
    private readonly AddDocumentViewModel _vm;
    private readonly User _user;
    public SubmitDocumentDetailCommand(AddDocumentViewModel vm, User user) 
        => (_vm, _user) = (vm, user);
    public override void Execute(object? parameter)
    {
        DocumentDetail documentDetail = new(
            new Document(_vm.SelectedDocumentName),
            _vm.DocumentNumber,
            _vm.IssuedDate,
            _vm.IssuedDate);


        _user.AddDocumentDetail(documentDetail);
    }
}
