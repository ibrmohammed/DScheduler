$.validator.addMethod("recurrencepatternrequired", function (value, element, params) {
    return value;
});
$.validator.unobtrusive.adapters.add("recurrencepatternrequired", ["otherpropertyname"], function (options) {
    debugger;
    options.rules["recurrencepatternrequired"] = "#" + options.params.otherpropertyname;
    options.messages["recurrencepatternrequired"] = options.message;
});