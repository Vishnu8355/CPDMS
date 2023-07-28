
//*****************Global Method******************
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};
//function ValidateIndianPincode(zip) {
//    var pattern = "^\d{6}$";
//    return pattern.test(zip);
//}
var Title = "Khanij";
var Size = "small";
//*****************End Global*********************


////*************For TextBox Validation*************
////Blank Field Validation
function blankFieldValidation(ControlName, MessageControlName) {
    if ($('#' + ControlName).val() == '') {
        debugger;
        swal('Please Enter ' + MessageControlName + '  !');
        $(".swal-button").click(function () {
            $('#' + ControlName).focus();
        });
        return false;
    }
    else {
        return true;
    }
}

////*************For Dropdownlist Validation*************
////Blank Field Validation
function FileUploadValidation(ControlName, FieldName) {
    debugger;
    if ($('#' + ControlName)[0].files.length === 0) {
        swal('Please Upload ' + FieldName + '  !');
        $(".swal-button").click(function () {
            $('#' + ControlName).focus();
        });
        return false;
    } else {

        return true;
    }
}

function DropDownValidation(ControlName, FieldName) {
    if ($('#' + ControlName).val() == '' || $('#' + ControlName).val() == '0' || $('#' + ControlName).val() == '0' || $('#' + ControlName).val() == '---Select---') {

        swal('Please select ' + FieldName + '  !');
        $(".swal-button").click(function () {
            $('#' + ControlName).focus();
        });
       
        return false;
    }
    else {
        return true;
    }
   
}
function checkInput(ControlName, MessageControlName) {
    debugger;
    if (!$('input[name=' + ControlName + ']:checked').val()) {
        swal('Please select ' + MessageControlName + '  !');
        $(".swal-button").click(function () {
            $('#' + ControlName).focus();
        });
        return false;
    }
    else {
        return true;
    }
}
function SuccessAlert(Message) {
    if (Message == "Duplicate Record Found") {
        swal("", Message, "error");
    }
    else if (Message.indexOf("DEPENDANT") != -1) {
        swal("", Message, "error");
    }
    else if (Message.indexOf("Rejected") != -1) {
        swal("", Message, "error");
    }
    else if (Message.indexOf("Deleted") != -1) {
        swal("", Message, "error");
    }
    else {
        swal('', Message, 'success');
    }
   
    return false;
}
function ConfirmBox() {
    //Warning Message
    debugger;
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this imaginary file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }, function (isConfirm) {
        alert(isConfirm)
    });
   
}

function WhiteSpaceValidation1st(ControlName, MessageControlName) {
    if ($('#' + ControlName).val().charAt(0) == ' ') {
        swal('White Space is not allowed in first place of ' + MessageControlName + ' !');
        $(".sa-confirm-button-container button").click(function () {
            $('#' + ControlName).focus();
        });
        return false;
    }
    else {
        return true;
    }
}


////*********End Textbox Validation************************
//function SpecialCharacter1st(ControlName, MessageControlName) {
//    if ($('#' + ControlName).val().charAt(0) == "'") {
//        jAlert('<strong> ' + MessageControlName + ' is not allowed! </strong>', Title);
//        return false;
//    }
//    else {
//        return true;
//    }
//}








// JScript File
// JScript File

//==========================================================================

// Declare patterns for different Regular Expression

var PatternsDict = new Object()


// mathes USA telephone no.
PatternsDict.telpat = /^(\d{10}|(\d{3}-\d{3}-\d{4}))?$/
// example:-325-672-6433

// mathes telephone Indian no.
PatternsDict.telpatIND = /^((\+){1}[1-9]{1}[0-9]{0,1}[0-9]{0,1}(\s){1}[\(]{1}[1-9]{1}[0-9]{1,5}[\)]{1}[\s]{1})[1-9]{1}[0-9]{4,9}$/
// example:+91 (22) 24440444

// matches numeric
PatternsDict.numericpat = "^\d*$" // Any number is allowed, but are optional

// matches white space
PatternsDict.whitespacepat = /\s+/

// matches zip code
PatternsDict.zippat = /^(\d{5}|\d{9}|(\d{5}-\d{4}))?$/
//example:-78731
//PatternsDict.zippat = "^(\d{5}|\d{9}|(\d{5}-\d{4}))?$"

// matches IP address
PatternsDict.IPpat = /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/

// matches hex number
PatternsDict.hexpat = "^([a-fA-F0-9]+)?$"

// matches any alphanumeric character,hyphen(-) or an underscore(_)
// including white space
PatternsDict.validpat = "^[a-zA-Z0-9-_]+$"

// matches required field
PatternsDict.requiredpat = "^((/\s+)|'')?$"

// matches character
PatternsDict.charpat = /^[a-zA-Z]+$/

//PatternsDict.urlpat="(?<protocol>http(s)?|ftp)://(?<server>([A-Za-z0-9-]+\.)*(?<basedomain>[A-Za-z0-9-]+\.[A-Za-z0-9]+))+((/?)(?<path>(?<dir>[A-Za-z0-9\._\-]+)(/){0,1}[A-Za-z0-9.-/]*)){0,1}"
//PatternsDict.urlpat="/http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?/"







// mathes email
var emailpat = /^[A-Za-z0-9\-_\.]+@+[A-Za-z0-9\-\.]+\.+[A-Za-z]{2,10}$/

// matches unsigned float
var ufloatpat = /^((\d+(\.\d*)?)|((\d*\.)?\d+))$/

// matches signed float
var sfloatpat = /^(((\+|\-)?\d+(\.\d*)?)|((\+|\-)?(\d*\.)?\d+))$/

// PatternsDict.datepat=/^([1-9]|0[1-9]|[12][1-9]|3[01])\D([1-9]|0[1-9]|1[012])\D(19[0-9][0-9]|20[0-9][0-9])$/

const IndianZip = /^[1-9][0-9]{5}$/
const IndianMob = /^[6-9]\d{9}$/
// End of pattern declaration
//=================================================================================


// Check for valid email format

function isEmail(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = emailpat

    if (objregExp.test(strInput)) {
        return true

    }
    alert(msg)
    Object.focus()
    return false

}
// Check for valid indianzip

function isIndianZip(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(Object) == "") {
        return true
    }

    var objregExp = new RegExp(IndianZip) 
  
    if (objregExp.test(Object)) {
        return true

    }
    else {
        return false
    }

}
function isIndianMobile(Object, msg) {

    //var strInput = new String(Object.value)

    if (trim(Object) == "") {
        return true
    }

    var objregExp = new RegExp(IndianMob)
   
    if (objregExp.test(Object)) {
        return true

    }
    else {
        return false
    }

}
// Checks a character type field

function isChar(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.charpat)

    if (objregExp.test(strInput)) {

        return true

    }

    alert(msg)
    Object.focus()
    return false

}

// Check if field contains any character along with alplanumeric and -/_
// including white space

function isValid(Object, msg) {

    var strInput = new String(Object.value)
    var objregExp = new RegExp(PatternsDict.validpat)


    if (objregExp.test(strInput)) {

        return true

    }

    alert(msg)
    Object.focus()
    return false

}

// Checks mandatory field

function isRequired(Object, msg) {
    var strInput = trim(new String(Object.value))
    var objregExp = new RegExp(PatternsDict.requiredpat)


    if (objregExp.test(strInput)) {
        alert(msg)
        Object.focus()
        return false

    }

    return true

}

//=================================

// Checks valid hexa decimal number

function isHex(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {

        return true
    }

    var objregExp = new RegExp(PatternsDict.hexpat)
    //alert(objregExp)
    if (objregExp.test(strInput)) {
        return true

    }

    alert(msg)
    Object.focus()
    return false

}

// Checks valid IP address


function isValidIP(Object, msg) {
    var ipaddr = new String(Object.value)

    if (trim(ipaddr) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.IPpat)

    if (objregExp.test(ipaddr)) {
        var parts = ipaddr.split(".");
        if (parseInt(parseFloat(parts[0])) == 0) {
            alert(msg);
            return false;
        }
        for (var i = 0; i < parts.length; i++) {
            if (parseInt(parseFloat(parts[i])) > 255) {
                alert(msg);
                return false;
            }
        }
        return true

    }
    else {
        alert(msg)
        Object.focus()
        return false

    }
}



// Checks for valid zip no.
function isUSAZip(Object, msg) {
    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.zippat)


    if (objregExp.test(strInput)) {
        return true

    }

    alert(msg)
    Object.focus()
    return false

}


// Checks for white space in first place

function isWhitespace1st(Object, msg) {
    var strInput = new String(Object.value)
    var objregExp = new RegExp(PatternsDict.whitespacepat)
    if (objregExp.test(strInput)) {
        if (strInput.charAt(0) == " ") {
            if (msg != null)
                alert(msg);
            Object.focus()
            return false
        }
    }
    return true

}

// Checks for white space every where or at any place

function isWhitespace(Object, msg) {
    var strInput = new String(Object.value)
    var objregExp = new RegExp(PatternsDict.whitespacepat)
    if (objregExp.test(strInput)) {
        if (msg != null)
            alert(msg);
        Object.focus()
        return false
    }
    return true

}

// Checks for numeric input


/*
function isNumeric(Object,msg)

{
var strInput   = new String(Object.value)
var objregExp  = new RegExp(PatternsDict.numericpat)

if(objregExp.test(strInput))

{
return true

}

alert(msg)
Object.focus()
return false

}

*/
// Checks for USA telephone number

function isUSATel(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.telpat)

    if (objregExp.test(strInput)) {
        return true

    }

    alert(msg)
    Object.focus()
    return false

}

// Checks for INDIAN telephone number

function isINDTel(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.telpatIND)

    if (objregExp.test(strInput)) {
        return true

    }

    alert(msg)
    Object.focus()
    return false

}

// Checks partial phone number

function isFilled(Object, msg) {
    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = new RegExp(PatternsDict.telpat)

    if (objregExp.test(strInput)) {
        return true

    }

    alert(msg)
    Object.focus()
    return false
}


// =======================================================================================
// This function is used to change any text to Uppercase text

function UpperCase(toconvert) {
    text = new String(toconvert);
    toconvert = text.toUpperCase();

    return toconvert;
}


// Check for numeric field

function isNumeric(Object, length, msg) {
    var strInput = new String(Object.value)

    //if(strInput.length > 0 && !isWhitespace (Object,"White Space Not allowed"))
    if (strInput.length > 0) {
        if (strInput.length > length) {
            alert("Field must be " + length + " characters long")
            Object.focus()
            return false
        }

        for (i = 0; i < strInput.length; i++) {
            if (strInput.charAt(i) < '0' || strInput.charAt(i) > '9') {
                alert(msg)
                Object.focus()
                return false
            }
        }
    }
    return true
}



// Check whether Passwords are matched

function isPwdMatch(pwd, cpwd, msg) {

    if (pwd.value != cpwd.value) {
        alert(msg);
        cpwd.focus()
        return false
    }
    else
        return true
}



// Check if the field is of min length

function isMinlen(Object, len, msg) {
    strInput = trim(new String(Object.value))
    sLength = strInput.length
    if (sLength < len) {
        alert(msg)
        Object.focus()
        return false
    }
    return true
}

// Check if the maximum length of the field

function isMaxlen(Object, len, msg) {
    strInput = trim(new String(Object.value))
    sLength = strInput.length
    if (sLength > len) {
        alert(msg)
        Object.focus()
        return false
    }
    return true
}

// Check if the field is of fixed length

function isReslen(Object, len, msg) {
    strInput = trim(new String(Object.value))
    sLength = strInput.length
    if (sLength != len) {
        alert(msg)
        Object.focus()
        return false
    }
    return true
}

// Check if two fields are indentical

function isSimilar(Object1, Object2, msg) {
    strInput1 = new String(Object1.value)
    strInput2 = new String(Object2.value)

    if (strInput1.valueOf() == strInput2.valueOf()) {
        alert(msg)
        Object2.focus()
        return false
    }

    return true
}

// Check if two or more email ids are indentical

function isEmailSimilar(str1, str2, str3, str4, str5, msg) {

    strInput1 = new String(str1.value)
    strInput2 = new String(str2.value)
    strInput3 = new String(str3.value)
    strInput4 = new String(str4.value)
    strInput5 = new String(str5.value)


    var fstr, sstr;
    for (i = 1; i <= 5; i++) {
        fstr = new String(eval("strInput" + i))

        for (j = i + 1; j <= 5; j++) {

            sstr = new String(eval("strInput" + j))

            if (fstr.valueOf() != "" && sstr.valueOf() != "") {

                if (fstr.valueOf() == sstr.valueOf()) {
                    alert(msg)
                    if (j == 1)
                        str1.focus()
                    else if (j == 2)
                        str2.focus()
                    else if (j == 3)
                        str3.focus()
                    else if (j == 4)
                        str4.focus()
                    else if (j == 5)
                        str5.focus()

                    return false
                }
            }
        }
    }
    return true
}


//==Check whether Pwd Question & Ans. are entered..

function isValidQA(Object1, Object2, msg1, msg2) {
    //	Object = new String(val1.value)
    //	str2 = new String(val2.value)
    var str1 = new String(Object1.value)
    var str2 = new String(Object2.value)

    //if(Object.length > 0 || str2.length > 0)
    //{
    if (str1.length == 0) {
        alert(msg1)
        Object1.focus()
        return false
    }
    else if (str2.length == 0) {
        alert(msg2)
        Object2.focus()
        return false
    }
    else
        return true
    //}return true
} //End of ffunction isValidQA



function trim(strString) {
    var strCopy = new String(strString)
    strCopy = strCopy.replace(/^\s+/, "")
    strCopy = strCopy.replace(/\s+$/, "")
    return strCopy.toString()
}


//checks number of days in a month, and leap year related validations.
function isDate(Day, Mon, Yr) {
    var rem;
    var dateOk = true;
    if (Yr.length != 4) {
        alert("PLease Check! The Year you entered is Invalid.It should be in 'YYYY' format!")
        dateOk = false;
    }
    if (Day > 31) {
        alert("PLease Check! The Day you entered is Invalid.")
        dateOk = false;
    }
    if (Mon.length != 2) {
        alert("PLease Check! The Month you entered is Invalid.It should be in 'MM' format!")
        dateOk = false;
    }
    if (Mon > 12 || Mon < 01) {
        alert("PLease Check! The Month you entered is Invalid.")
        dateOk = false;
    }
    else {
        if (Day == 31) {
            if ((Mon == "02") || (Mon == "04") || (Mon == "06") || (Mon == "09") || (Mon == "11")) {
                dateOk = false;
                alert("PLease Check! The month you entered doesn't have 31 days.")
            }
        }
        else {
            if ((Day > 29) && (Mon == "02")) {
                dateOk = false;
                alert("PLease Check! The month you entered doesn't have " + Day + " days.")
            }
            else {
                if ((Day == 29) && (Mon == "02")) {
                    rem = Yr % 400;
                    if (rem == 0)
                        dateOk = true;
                    else {
                        rem = Yr % 100;
                        if (rem == 0)
                            dateOk = true;
                        else {
                            rem = Yr % 4;
                            if (rem == 0)
                                dateOk = true;
                            // alert("February can have 29 days in a leap year only. Please select a leap year")
                            else {
                                dateOk = false;
                                alert("February can have 29 days in a leap year only. Please select a leap year")
                            }
                        }

                    }
                }
            }
        }
        return dateOk
    }  //end of month check condition
} // end function



function isDateinMMM(day, month, year) {

    var isValid = true;

    var enteredDate = new Date(day + " " + month + " " + year);

    if (enteredDate.getDate() != day) {

        isValid = false;

    }

    return isValid;

}


// Check for valid signed float format

function isSignedFloat(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = sfloatpat

    if (objregExp.test(strInput)) {
        return true

    }
    alert(msg)
    Object.focus()
    return false

}

// Check for valid unsigned float format

function isUnSignedFloat(Object, msg) {

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    var objregExp = ufloatpat

    if (objregExp.test(strInput)) {
        return true

    }
    alert(msg)
    Object.focus()
    return false

}


function Replace(str1, str2, str3) {
    str1 = str1.replace(new RegExp(str2), str3);
    return str1
}

function isNumInRange(Object, low, high, msg)

// Results: alert if textBox does not contain an integer & clears contents
{

    var strInput = new String(Object.value)

    if (trim(strInput) == "") {
        return true
    }

    strInput = parseFloat(strInput);

    Object.value = strInput

    if (isNaN(strInput)) {
        Object.value = 0
        //return true
    }



    if (isNaN(high) && !isNaN(strInput)) {

        if ((high.toUpperCase() == "LT") || (high == "<"))
            Operator = "<"
        if ((high.toUpperCase() == "LE") || (high == "<="))
            Operator = "<="
        if ((high.toUpperCase() == "GT") || (high == ">"))
            Operator = ">"
        if ((high.toUpperCase() == "GE") || (high == ">="))
            Operator = ">="
        else if ((high.toUpperCase() == "EQ") || (high == "="))
            Operator = "=="

        if (!eval(strInput + " " + Operator + " " + low)) {
            alert(msg + " i.e. in between " + "" + low + "" + "-" + "" + high + "")
            Object.focus()
            return false
        }

        return true

    }


    if (isNaN(strInput) || (strInput < low) || (strInput > high)) {
        alert(msg + " i.e. in between " + "" + low + "" + "-" + "" + high + "")

        Object.focus()
        return false

    }

    return true
}


function isIntInRange(Object, low, high, msg)

// Results: alert if textBox does not contain an integer in range & clears
{
    var strInput = new String(Object.value)
    var Operator

    if (trim(strInput) == "") {
        return true
    }


    strInput = parseInt(strInput, 10);

    Object.value = strInput

    if (isNaN(strInput)) {
        Object.value = 0
        //return true
    }



    if (isNaN(high) && !isNaN(strInput)) {

        if ((high.toUpperCase() == "LT") || (high == "<"))
            Operator = "<"
        if ((high.toUpperCase() == "LE") || (high == "<="))
            Operator = "<="
        if ((high.toUpperCase() == "GT") || (high == ">"))
            Operator = ">"
        if ((high.toUpperCase() == "GE") || (high == ">="))
            Operator = ">="
        else if ((high.toUpperCase() == "EQ") || (high == "="))
            Operator = "=="

        if (!eval(strInput + " " + Operator + " " + low)) {
            alert(msg + " i.e. in between " + "" + low + "" + "-" + "" + high + "")
            Object.focus()
            return false
        }


        return true

    }

    if (isNaN(strInput) || (strInput % 1 != 0) || (strInput < low) || (strInput > high)) {
        alert(msg + " i.e. in between " + "" + low + "" + "-" + "" + high + "")

        Object.focus()
        return false

    }

    return true
}

//Checks for multiple checked checkboxes

function isMultipleChecked(Object, MsgOption, msg) {
    var intNoOfLines = 0;
    var NumChecked;

    NumChecked = 0;

    if (Object) {
        intNoOfLines = Object.length;
    }

    if (isNaN(intNoOfLines)) {
        intNoOfLines = 1;

    }

    if (intNoOfLines == 1) {
        if (Object.checked) {
            NumChecked++;
        }
    }
    else {
        for (i = 0; i < intNoOfLines; i++) {

            if (Object[i].checked) {

                NumChecked++;

            }
        }
    }

    if (NumChecked > 1) {
        if (MsgOption) {

            if (trim(msg) != "" || isMultipleChecked.arguments.length > 2)
                alert(msg);
        }

        return true;
    }

    if (!MsgOption) {

        if (trim(msg) != "" && isMultipleChecked.arguments.length > 2)
            alert(msg);
    }

    return false;

}
//Checks at lease one checkbox/radio button has been checked or not

function isAtleastOneChecked(Object, msg) {

    var intNoOfLines = 0;
    var boolChecked = false;

    if (Object) {
        intNoOfLines = Object.length;
    }

    if (isNaN(intNoOfLines)) {
        intNoOfLines = 1;

    }


    if (intNoOfLines == 1) {
        if (Object.checked) {
            boolChecked = true;
        }
    }
    else {
        for (i = 0; i < intNoOfLines; i++) {

            if (Object[i].checked) {

                boolChecked = true;
                break;

            }
        }
    }

    if (boolChecked) {

        return true;
    }

    alert(msg);
    return false;
}

//==== Checks and unchecks all the check boxes

function selectsAll(Object1, Object2) {

    var intNoOfItems = 0;

    if (Object2) {
        intNoOfItems = Object2.length;
    }

    if (isNaN(intNoOfItems)) {
        intNoOfItems = 1;

    }

    if (Object1.checked) {
        if (intNoOfItems == 1) {
            Object2.checked = true;
        }
        else {
            for (i = 0; i < intNoOfItems; i++) {
                Object2[i].checked = true;
            }
        }

        Object1.checked = true;



    }
    else {
        if (intNoOfItems == 1) {
            Object2.checked = false;
        }
        else {
            for (i = 0; i < intNoOfItems; i++) {
                Object2[i].checked = false;
            }
        }

        Object1.checked = false;



    }
}

function chkAllCheckBoxes(Object1, Object2) {

    var TB = TO = 0;
    var intNoOfItems = 0;

    if (Object2) {
        intNoOfItems = Object2.length;
    }

    if (isNaN(intNoOfItems)) {
        intNoOfItems = 1;

    }

    for (var i = 0; i < intNoOfItems; i++) {
        TB++;

        if (intNoOfItems == 1) {
            if (Object2.checked)
                TO++;
        }
        else {
            if (Object2[i].checked)
                TO++;
        }
    }

    if (TO == TB)
        Object1.checked = true;
    else
        Object1.checked = false;
}
//check dropdown is selected or not
function isSelectDropDown(Object, msg) {
    if (Object == false || Object.selectedIndex == 0) {
        alert(msg)
        Object.focus()
        return false
    }
    return true
}
//checks Single Quote
function isSingleQuote(Object, msg) {
    var str1 = trim(new String(Object.value))
    for (var i = 0; i < str1.length; i++) {
        var ch = str1.substring(i, i + 1);
        if (ch == "'") {
            alert(msg);
            Object.focus();
            return false;
        }
    }
    return true;
}

//checks valid URL
function isValidURL(Object, msg) {
    var str1 = trim(new String(Object.value))
    //if ((str2.value == "") ||(str2.value.indexOf("www.") == -1) ||(str2.value.indexOf(".") == -1)) 
    if ((str1 == "") || (str1.indexOf("www.") == -1) || (str1.indexOf(".") == -1)) {
        alert(msg);
        Object.focus();
        return false;
    }
    return true;

}

//checks valid URL

//  function isValidURL(Object,msg)
// {

//   var strInput   = new String(Object.value)

//   if (trim(strInput) == "")

//     {
//       return true
//     }

//   var objregExp  = new RegExp(PatternsDict.urlpat)
//   if(objregExp.test(strInput))

//     {
//       return true

//     }

//     alert(msg)
//     Object.focus()
//     return false

// }
//selects or checks all checkboxes in a grid
function isSelectAll(CheckBoxControl, GridId, Formname) {
    var FormNM = document.getElementById(Formname)
    if (CheckBoxControl.checked == true) {
        var i;
        for (i = 0; i < FormNM.elements.length; i++) {
            if ((FormNM.elements[i].type == 'checkbox') && (FormNM.elements[i].name.indexOf(GridId) > -1)) {
                FormNM.elements[i].checked = true;
            }
        }
    }
    else {
        var i;
        for (i = 0; i < FormNM.elements.length; i++) {
            if ((FormNM.elements[i].type == 'checkbox') && (FormNM.elements[i].name.indexOf(GridId) > -1)) {
                FormNM.elements[i].checked = false;
            }
        }
    }
}

//=======================END OF INPUT VALIDATION SERVICE==============================

// JScript File

var strSizeMinAlert = "<Field> can not be less than <n> characters!"
var strSizeMinResAlert = "<Field> must contain <n> characters!"
var strSizeMaxAlert = "<Field> can not be more than <n> characters long!"
var strSizeMaxResAlert = "<Field> should be maximum <n> characters long!"
var strResAllowAlert1 = "Invalid characters entered in <Field>. Allows only <\"_\",\"-\",\"/\" > as special characters!"
var strResAllowAlert2 = "<Field> allows only  <\"_\",\"-\",\"/\" >characters!"
var strResAllowAlert3 = "Special characters \"-,_\" are only allowed!"
var strResNotAllowAlert1 = "Invalid characters entered in <Field>. Characters <\"-\",\"&\",\"$\">, are not allowed!"
var strResNotAllowAlert2 = "<Field> can not contain <\"-\",\"&\",\"$\"> characters!"
var strResNotAllowAlert3 = "<Field> does not accepts space(s)!"
var strNumericAlert = "<Field> accepts only numeric values!"
var strAlphabeticAlert1 = "<Field> should be alphabetic!"
var strAlphabeticAlert2 = "<Field> accepts only alphabetic values!"
var strMandatoryAlert1 = "<Field> is mandatory!"
var strMandatoryAlert2 = "<Field> can not be left blank!"
var strMandatoryAlert3 = "<Field> is a required filed!"
var strPositiveAllowAlert = "<Field> accepts positive values only!"
var strNegetiveAllowAlert = "<Field> accepts negative values only!"
var strInvalidFomatAlert = "Invalid format. Please enter <Field> as <AA-BB#CCC@DD>!"
var strNotAllowSimilarAlert = "<Field1> and <Field2> must not be same!"
var strDoNotMatchAlert = "Information mismatched in <Field1>. Please re-enter <Field1>!"
var strInvalidSignedFloatAlert = "<Field> should be a floating point (real) number. (Integers also OK.)!"
var strInvalidUnSignedFloatAlert = "<Field> should be an unsigned floating point (real) number. (Integers also OK.)!"
var strSplChar = "Special Character is not Allowed !"

//MESSAGES FOR DATE SERVICE//
var strValidDate = "Valid date!";
var strInvalidDateFormat = "Invalid date format!";
var strInvalidMonth = "Invalid month!";
var strInvalidFebDays = "February cannot have 29 days other than a leap year!";
var strInvalidMonthDays = "Invalid number of days in the specified month!";
var strInvalidYear = "Invalid year!";
var strInvalidParameter = "Invalid parameter!";
var strFormatMismatch = "Format mismatch!";
var strInternalError = "Internal error!";
var strSimilarEmail = "Email Id should be unique!"
//------------------Message added by Pramod-------------------------------------------------
var strEmail = "Invalid Email_Id!";
var strHex = "Invalid Entry.HexaDecimalNos. only contains a-f,A-F,0-9 characters"
var strIp = "Invalid IP address!"
var strZip = "Invalid ZipCode!"
var strWhiteSpace = "White space(s) not allowed!"
var strWhiteSpace1st = "White space(s) not allowed in first place!"
var strTelNo = "Invalid telephone number!"
var strFilled = "Partial phone number not filled properly!"
var strAllowSimilarAlert = "<Field1> and <Field2> must be same.Please Re_enter <Field2>!"
var strNuminRange = "<Field> must be with in specified range!"
var strSelecteDropDown = "Please select <Field>!"
var strSingleQuote = "Single Quote not allowed!"
var strValidURL = "Enter URL in Website Format!.Ex-: www.google.com"
var strChecked = "No Record is Selected"
var strCompareDate = "Invalid Date Range!<Field1> cannot be after <Field2>!"
//=========================================END OF  STANDRD ERROR ALERT==========================================
function InputValidator() {
    this.isEmail = isEmail;
    this.isEmailSimilar = isEmailSimilar
    this.isChar = isChar;
    this.isValid = isValid;
    this.isRequired = isRequired;
    this.isHex = isHex;
    this.isValidIP = isValidIP;
    this.isUSAZip = isUSAZip;
    this.isWhitespace = isWhitespace;
    this.isWhitespace1st = isWhitespace1st;
    this.isNumeric = isNumeric;
    this.isUSATel = isUSATel;
    this.isINDTel = isINDTel;
    this.isFilled = isFilled; //Added By Pramod Kumar Pradhan on 6th jan'07
    this.isPwdMatch = isPwdMatch;
    this.isMinlen = isMinlen;
    this.isReslen = isReslen;
    this.isMaxlen = isMaxlen;
    this.isSimilar = isSimilar;
    this.isDateinMMM = isDateinMMM;
    this.isDate = isDate;
    this.UpperCase = UpperCase;
    this.isValidQA = isValidQA;
    this.trim = trim;
    this.Replace = Replace;
    this.isSignedFloat = isSignedFloat;
    this.isUnSignedFloat = isUnSignedFloat;
    this.isNumInRange = isNumInRange;
    this.isIntInRange = isIntInRange;
    this.isAtleastOneChecked = isAtleastOneChecked;
    this.selectsAll = selectsAll;
    this.chkAllCheckBoxes = chkAllCheckBoxes;
    this.isMultipleChecked = isMultipleChecked;
    this.isSelectDropDown = isSelectDropDown;
    this.isSingleQuote = isSingleQuote;
    this.isValidURL = isValidURL;
    this.isSelectAll = isSelectAll;
    this.isChecked = isChecked;
    this.strChecked = strChecked;
    this.isDateCompareinCalendar = isDateCompareinCalendar;
}

function ErrorAlert() {
    this.SizeMinAlert = strSizeMinAlert;
    this.SizeMinResAlert = strSizeMinResAlert;
    this.SizeMaxAlert = strSizeMaxAlert;
    this.SizeMaxResAlert = strSizeMaxResAlert;
    this.ResAllowAlert1 = strResAllowAlert1;
    this.ResAllowAlert2 = strResAllowAlert2;
    this.ResAllowAlert3 = strResAllowAlert3;
    this.ResNotAllowAlert1 = strResNotAllowAlert1;
    this.ResNotAllowAlert2 = strResNotAllowAlert2;
    this.ResNotAllowAlert3 = strResNotAllowAlert3;
    this.NumericAlert = strNumericAlert;
    this.AlphabeticAlert1 = strAlphabeticAlert1;
    this.AlphabeticAlert2 = strAlphabeticAlert2;
    this.MandatoryAlert1 = strMandatoryAlert1;
    this.MandatoryAlert2 = strMandatoryAlert2;
    this.MandatoryAlert3 = strMandatoryAlert3;
    this.PositiveAllowAlert = strPositiveAllowAlert;
    this.NegetiveAllowAlert = strNegetiveAllowAlert;
    this.InvalidFomatAlert = strInvalidFomatAlert;
    this.NotAllowSimilarAlert = strNotAllowSimilarAlert;
    this.DoNotMatchAlert = strDoNotMatchAlert;
    this.InvalidSignedFloatAlert = strInvalidSignedFloatAlert
    this.InvalidUnSignedFloatAlert = strInvalidUnSignedFloatAlert
    //---------------------Added By Pramod-------------------------------------------------------
    this.validEmail = strEmail
    this.validHex = strHex
    this.validIP = strIp
    this.validZIP = strZip
    this.whitespace = strWhiteSpace
    this.whitespace1st = strWhiteSpace1st
    this.validPhoneNo = strTelNo
    this.validFill = strFilled
    this.matchpwd = strAllowSimilarAlert
    this.similarEmailalert = strSimilarEmail
    this.numinRangealert = strNuminRange
    this.strSelecteDropDown = strSelecteDropDown
    this.strSingleQuote = strSingleQuote
    this.strValidURL = strValidURL
    this.isChecked = isChecked;
    this.strChecked = strChecked;
    this.strSplChar = strSplChar;
    this.strCompareDate = strCompareDate;
}
//====================================END OF TEST PAGE=======================================
////============1.Function checking blank field for textboxes/areas==============================
//function blankFieldValidation(Controlname, Fieldname) {

//    var objfrm = document.getElementById(Controlname);
//    var objFieldname = Fieldname;
//    var flag;
//    flag = false;

//    var objValidator = new InputValidator();
//    var objError = new ErrorAlert();
//    if
//    (
//       objValidator.isRequired(objfrm, objValidator.Replace(objError.MandatoryAlert2, "<Field>", objFieldname))

//    ) {
//        objError = null
//        objValidator = null
//        //alert ("Form has been validated successfully.")
//        flag = true
//    }

//    objError = null
//    objValidator = null
//    return flag
//}
//============2.Function checking maximum length of a field ==============================
function MaxlengthValidation(Controlname, Fieldname, maxlen) {
    //var objform=document.getElementById(Formname);
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var objmaxlen = maxlen

    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isMaxlen(objfrm, objmaxlen, objValidator.Replace(objValidator.Replace(objError.SizeMaxAlert, "<Field>", objFieldname), "<n>", objmaxlen))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}

//============3.Function checking minimum length of a field ==============================

function MinlengthValidation(Controlname, Fieldname, minlen) {
    //var objform=document.getElementById(Formname);
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var objminlen = minlen

    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isMinlen(objfrm, objminlen, objValidator.Replace(objValidator.Replace(objError.SizeMinAlert, "<Field>", objFieldname), "<n>", objminlen))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//============4.Function checking Restricted length of a field ==============================

function ReslengthValidation(Controlname, Fieldname, reslen) {
    //var objform=document.getElementById(Formname);
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var objminlen = reslen

    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isReslen(objfrm, objminlen, objValidator.Replace(objValidator.Replace(objError.SizeMinResAlert, "<Field>", objFieldname), "<n>", objminlen))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}

//============5.Function checking EmailValidation of a field ==============================
function EmailValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
      objValidator.isEmail(objfrm, objError.validEmail)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}


//====================6.Function Checks a character type field==============================
//function CharValidation(Controlname, Fieldname) {
//    var objfrm = document.getElementById(Controlname);
//    var objFieldname = Fieldname;
//    var flag;
//    flag = false;

//    var objValidator = new InputValidator();
//    var objError = new ErrorAlert();
//    if
//    (
//     objValidator.isChar(objfrm, objValidator.Replace(objError.AlphabeticAlert2, "<Field>", objFieldname))
//    ) {
//        objError = null
//        objValidator = null
//        //alert ("Form has been validated successfully.")
//        flag = true
//    }

//    objError = null
//    objValidator = null
//    return flag
//}

//====================6.Function Checks a character type field==============================
function CharValidation(Controlname, Fieldname) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
     objValidator.isChar(objfrm, objValidator.Replace(objError.AlphabeticAlert2, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}

//====================7.Function checking isValid Characters (Check if field contains any character except alplanumeric and -/_.including white space)==============================
function isValidCharValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    //var objFieldname=Fieldname;
    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    //objValidator.isValid(objfrm, objValidator.Replace(objError.ResAllowAlert3,"<Field>",objFieldname))
objValidator.isValid(objfrm, objError.ResAllowAlert3)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}

//====================8.Function checking HexaDecimal no's ==============================
function HexaValidation(Controlname) {

    var objfrm = document.getElementById(Controlname);

    //var objFieldname=Fieldname;

    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();


    if
    (

      objValidator.isHex(objfrm, objError.validHex)

    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}

//====================9.Function checking valid IP adress ==============================
function IPValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isValidIP(objfrm, objError.validIP)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}


//====================10.Function checking USA ZIP CODE ==============================
function USAZIPCodeValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isUSAZip(objfrm, objError.validZIP)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}

//====================11.Function checking WhiteSpaces ==============================
function WhiteSpaceValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
           objValidator.isWhitespace(objfrm, objError.whitespace)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}


//====================12.Function checking USA TELEPHONE Number ==============================
function USATelNoValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();

    if
    (
  objValidator.isUSATel(objfrm, objError.validPhoneNo)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}


//====================13.Function checking IND TELEPHONE Number ==============================
function INDTelNoValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isINDTel(objfrm, objError.validPhoneNo)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}
//====================14.Function checking  isFilled (Checks partial phone number)==============================
//function isFilled_Validation(Controlname,Fieldname)
//  {
//    var objfrm=document.getElementById(Controlname);
//    var objFieldname=Fieldname;
//	var flag;
//	flag=false;
//	var objValidator = new InputValidator();
//	var objError 	 = new ErrorAlert();
//   if
//    (
//  objValidator.isFilled(objfrm,objValidator.Replace(objError.validFill,"<Field>",objFieldname))
//    )
//        {
//	  	objError = null
//		objValidator = null
//		alert ("Form has been validated successfully.")
//		flag=true
//        }
//	objError = null
//	objValidator = null
//	return flag
//  }

//====================15.Function for toUpperCase ==============================
function toUpperValidation(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var msg = objfrm.value
    var objValidator = new InputValidator();
    var str = objValidator.UpperCase(msg)
    alert(str)
}

//====================16.Function to check numeric values ==============================
function NumericValidation(Controlname, Fieldname, length) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var objlen = length


    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isNumeric(objfrm, objlen, objValidator.Replace(objError.NumericAlert, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//====================17.Function for to check match password ==============================
function PasswordValidation(Controlname1, Controlname2, Fieldname1, Fieldname2) {
    var objfrm1 = document.getElementById(Controlname1);
    var objfrm2 = document.getElementById(Controlname2);
    var objFieldname1 = Fieldname1;
    var objFieldname2 = Fieldname2;


    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isPwdMatch(objfrm1, objfrm2, objValidator.Replace(objValidator.Replace(objValidator.Replace(objError.matchpwd, "<Field2>", objFieldname2), "<Field2>", objFieldname2), "<Field1>", objFieldname1))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}

//====================18.Function for to not allow similar fields ==============================
function DisSimilarValidation(Controlname1, Controlname2, Fieldname1, Fieldname2) {
    var objfrm1 = document.getElementById(Controlname1);
    var objfrm2 = document.getElementById(Controlname2);
    var objFieldname1 = Fieldname1;
    var objFieldname2 = Fieldname2;


    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isSimilar(objfrm1, objfrm2, objValidator.Replace(objValidator.Replace(objError.NotAllowSimilarAlert, "<Field2>", objFieldname2), "<Field1>", objFieldname1))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}

//====================19.Function for checking unique values in different fields ==============================
function chkSimilarID(Controlname1, Controlname2, Controlname3, Controlname4, Controlname5) {
    var objfrm1 = document.getElementById(Controlname1);
    var objfrm2 = document.getElementById(Controlname2);
    var objfrm3 = document.getElementById(Controlname3);
    var objfrm4 = document.getElementById(Controlname4);
    var objfrm5 = document.getElementById(Controlname5);

    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
    objValidator.isEmailSimilar(objfrm1, objfrm2, objfrm3, objfrm4, objfrm5, objError.similarEmailalert)
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//====================20.Function for checking whether Pwd Question & Ans. are entered.(i.e two fields simultaneously are filled)==============================
function isValidQAValidation(Controlname1, Controlname2, Fieldname1, Fieldname2) {
    var objfrm1 = document.getElementById(Controlname1);
    var objfrm2 = document.getElementById(Controlname2);
    var objFieldname1 = Fieldname1;
    var objFieldname2 = Fieldname2;


    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (

    objValidator.isValidQA(objfrm1, objfrm2, objValidator.Replace(objError.MandatoryAlert2, "<Field>", objFieldname1), objValidator.Replace(objError.MandatoryAlert2, "<Field>", objFieldname2))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//====================21.Function for checking valid date in dd-MMM-YYYY format==============================
function isValidDateValidation(Controlname1) {
    var objfrm1 = document.getElementById(Controlname1);
    var strdate = objfrm1.value
    var dt = strdate.split("/")
    var flag = false;

    var objValidator = new InputValidator();
    if
	(
	strdate != dt
	) {
        flag = objValidator.isDateinMMM(dt[0], dt[1], dt[2])
    }
    else {
        dt = strdate.split("-")
        flag = objValidator.isDateinMMM(dt[0], dt[1], dt[2])
    }
    if (flag == true)
        alert("Valid Date")
    else
        alert("invalid Date")
    objfrm1.focus()
    flag == false
    //	  	objValidator = null
    //		alert ("Form has been validated successfully.")
    //		flag=true
    //        }
    //	objValidator = null
    return flag
}

//====================22.Function for checking valid date in dd-MM-YYYY format==============================
function isValidDateValidation1(Controlname1) {
    var objfrm1 = document.getElementById(Controlname1);
    var strdate = objfrm1.value
    var dt = strdate.split("/")
    var flag = false;

    var objValidator = new InputValidator();
    if
	(
	strdate != dt
	) {
        flag = objValidator.isDate(dt[0], dt[1], dt[2])
    }
    else {
        dt = strdate.split("-")
        flag = objValidator.isDate(dt[0], dt[1], dt[2])
    }
    if (flag == true)
        alert("Valid Date")
    else
        alert("invalid Date")
    objfrm1.focus()
    flag == false
    //	  	objValidator = null
    //		alert ("Form has been validated successfully.")
    //		flag=true
    //        }
    //	objValidator = null
    return flag
}



//====================23.Function checking  Signed Float value==============================
function isSignedFloatValidation(Controlname, Fieldname) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isSignedFloat(objfrm, objValidator.Replace(objError.InvalidSignedFloatAlert, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}


//====================24.Function checking  Unsigned Float value==============================
function isUnSignedFloatValidation(Controlname, Fieldname) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isUnSignedFloat(objfrm, objValidator.Replace(objError.InvalidUnSignedFloatAlert, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}

//====================25.Function specifying Number with in a particular range==============================
function isNumInRangeValidation(Controlname, Low, High, Fieldname) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isNumInRange(objfrm, Low, High, objValidator.Replace(objError.numinRangealert, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}


//====================26.Function specifying An integer with in a particular range==============================
function isIntInRangeValidation(Controlname, Low, High, Fieldname) {
    var objfrm = document.getElementById(Controlname);
    var objFieldname = Fieldname;
    var flag;
    flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
  objValidator.isIntInRange(objfrm, Low, High, objValidator.Replace(objError.numinRangealert, "<Field>", objFieldname))
    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }
    objError = null
    objValidator = null
    return flag
}
//============27.Function checking select dropdown==============================

//function DropDownValidation(Controlname, Fieldname) {
//    var objfrm = document.getElementById(Controlname);
//    var objFieldname = Fieldname;
//    var flag;
//    flag = false;

//    var objValidator = new InputValidator();
//    var objError = new ErrorAlert();
//    if
//    (
//        objValidator.isSelectDropDown(objfrm, objValidator.Replace(objError.strSelecteDropDown, "<Field>", objFieldname))
//    ) {
//        objError = null
//        objValidator = null
//        //alert ("Form has been validated successfully.")
//        flag = true
//    }

//    objError = null
//    objValidator = null
//    return flag
//}

//============28.Function checking Single Quote==============================
function chkSingleQuote(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
       objValidator.isSingleQuote(objfrm, objError.strSingleQuote)

    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//============29.Function checking valid URL==============================
function chkURL(Controlname) {
    var objfrm = document.getElementById(Controlname);
    var flag;
    flag = false;

    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    if
    (
       objValidator.isValidURL(objfrm, objError.strValidURL)

    ) {
        objError = null
        objValidator = null
        //alert ("Form has been validated successfully.")
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
//============30.Function to check/select all checkboxes==============================

function SelectAll(chkboxControlname, GridID, FormName) {
    var objValidator = new InputValidator();
    objValidator.isSelectAll(chkboxControlname, GridID, FormName)
}
//====================31.Function checking WhiteSpaces in first place==============================

//function WhiteSpaceValidation1st(Controlname) {
//    var objfrm = document.getElementById(Controlname);
//    var flag;
//    flag = false;
//    var objValidator = new InputValidator();
//    var objError = new ErrorAlert();
//    if
//    (
//           objValidator.isWhitespace1st(objfrm, objError.whitespace1st)
//    ) {
//        objError = null
//        objValidator = null
//        //alert ("Form has been validated successfully.")
//        flag = true
//    }
//    objError = null
//    objValidator = null
//    return flag
//}
////=========================================================================
//Added By Pratik On 14-Jul-09  
function deSelectHeader(ChkID, GridId, Formname) {
    var FormId = document.getElementById(Formname);
    var i, j, k;
    j = 0;
    k = 0
    for (i = 0; i < FormId.elements.length; i++) {
        if ((FormId.elements[i].type == 'checkbox') && (FormId.elements[i].name.indexOf(GridId) > -1) && (FormId.elements[i].disabled == false)) {
            k = k + 1;
            if (FormId.elements[i].checked == false) {
                ChkID.checked = false;
            }
            else {
                j = parseInt(j) + 1;
            }
        }
    }
    if (j == k)
        ChkID.checked = true;
    return true;
}
//==========================================================================
//===========================Function checking checkbox checked or not=====================

function ConfirmCheck(FormName) {
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();
    var flag;
    flag = false;

    if (objValidator.isChecked(FormName, objError.strChecked)) {
        objError = null
        objValidator = null
        flag = true
    }

    objError = null
    objValidator = null
    return flag
}
function isChecked(Form, msg) {
    var FormName = document.getElementById(Form)

    for (var i = 0; i < FormName.elements.length; i++) {
        if (FormName.elements[i].type == 'checkbox')
            if (FormName.elements[i].checked == true)
                return true;
    }
    alert(msg);
    return false;
}
//======================
function IsSpecialCharacter(Object) {
    var Arr = new Array();
    var k;
    Arr = Object.split(',');
    for (k = 0; k < Arr.length; k++) {
        var str1 = trim(new String(document.getElementById(Arr[k]).value))
        for (var i = 0; i < str1.length; i++) {
            var ch = str1.substring(i, i + 1);
            if (ch == "'" || ch == ">" || ch == "<" || ch == "!" || ch == "^" || ch == "%" || ch == "?" || ch == "~" || ch == "@" || ch == "#") {
                alert('Special Characters are not allowed');
                document.getElementById(Arr[k]).focus();
                return false;
            }
        }
    }
    return true;
}
//=============================================
/*Character at First palce*/
/*Created by :Subrat Hota*/
function isCharfirst(Controlname) {
    // var objError = new ErrorAlert();
    //var msg = 'can not insert Number in First palce';  //objError.strFstchar
    var str2 = document.getElementById(Controlname);
    var strInput = new String(str2.value);
    for (var i = 0; i < strInput.length; i++) {
        var str = strInput.charAt(0)
        if (!isNaN(str)) {
            alert('Can not insert Number in First Palce')
            str2.focus()
            return false;
        }
        else {
            return true;
        }
    }
}
//==========================================
/*Special character Validation*/
/*Created by :Subrat Hota*/
function IsXmlNode(Controlname) {
    var objError = new ErrorAlert();
    var msg = objError.strSplChar
    var ValidChars = "~`!@#$%^&*()+=-[]\\\';,./{}|\":<>?-_";
    var str2 = document.getElementById(Controlname);
    var str1 = str2.value;
    var IsNumber = true;
    var Char;

    var position;

    for (i = 0; i < str1.length && IsNumber == true; i++) {
        Char = str1.charAt(i);
        position = ValidChars.indexOf(Char);

        if (position > -1) {
            IsNumber = false;
        }
    }

    if (IsNumber == false) {
        str1.IsValid = false;
        alert(objError.strSplChar)
        str2.focus();
        return false;
    }
    else {
        str1.IsValid = true;
        return true;
    }
}

function CheckDate4(date1, date2)  //Function to Check two Date whether greater or smaller
{
    if (date1.toString().substring(6, 10) >= date2.toString().substring(6, 10)) {
        if (date1.toString().substring(6, 10) == date2.toString().substring(6, 10)) {
            if (date1.toString().substring(0, 2) >= date2.toString().substring(0, 2)) {
                if (date1.toString().substring(0, 2) == date2.toString().substring(0, 2)) {
                    if (date1.toString().substring(3, 5) >= date2.toString().substring(3, 5))
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return false;
        }
        else
            return true;
    }
    else
        return false;
}


function formatDate4(dt)   //To get Correct Date Format for Calculation
{

    var strTemp = "";
    var strChar;
    var date1 = new Array(3);
    var j = 0;
    var strDateTo = dt;
    var todatelen = strDateTo.length;

    for (var i = 0; i <= todatelen; i++) {
        strChar = strDateTo.charAt(i);
        if (strChar == '-' || i == todatelen) {
            date1[j] = strTemp;
            strTemp = "";
            j = j + 1;
        }
        else {
            strTemp = strTemp + strChar;
        }

        if (strChar == " ")
            break;
    }

    switch (date1[1]) {
        case 'Jan': date1[1] = 01;
            break;
        case 'Feb': date1[1] = 02;
            break;
        case 'Mar': date1[1] = 03;
            break;
        case 'Apr': date1[1] = 04;
            break;
        case 'May': date1[1] = 05;
            break;
        case 'Jun': date1[1] = 06;
            break;
        case 'Jul': date1[1] = 07;
            break;
        case 'Aug': date1[1] = 08;
            break;
        case 'Sep': date1[1] = 09;
            break;
        case 'Oct': date1[1] = 10;
            break;
        case 'Nov': date1[1] = 11;
            break;
        case 'Dec': date1[1] = 12;
            break;
    }

    if (date1[0] < 10)
        date1[0] = date1[0].toString().substring(0);

    if (date1[1] < 10)
        date1[1] = '0' + date1[1].toString().substring(0);

    var conDate = date1[1] + "/" + date1[0] + "/" + date1[2];
    return (conDate);

}
function DigiDate4()  //Function to get Digital Date 
{
    if (navigator.appName == "Microsoft Internet Explorer") {
        var DigiDate = new Date()
        var year = DigiDate.getYear()
        var month = DigiDate.getMonth()
        var day = DigiDate.getDay()
        var dayNo = DigiDate.getDate()
        var date;
        var DayNoType;

        if ((dayNo == "1") || (dayNo == "21") || (dayNo == "31")) { DayNoType = "st" }

        else if ((dayNo == "2") || (dayNo == "22")) { DayNoType = "nd" }

        else if ((dayNo == "3") || (dayNo == "23")) { DayNoType = "rd" }

        else (DayNoType = "th")



        if (day == "1") { day = "Mon" }

        else if (day == "2") { day = "Tue" }

        else if (day == "3") { day = "Wed" }

        else if (day == "4") { day = "Thu" }

        else if (day == "5") { day = "Fri" }

        else if (day == "6") { day = "Sat" }

        else if (day == "0") { day = "Sun" }



        if (month == "0") { month = "Jan" }

        else if (month == "1") { month = "Feb" }

        else if (month == "2") { month = "Mar" }

        else if (month == "3") { month = "Apr" }

        else if (month == "4") { month = "May" }

        else if (month == "5") { month = "Jun" }

        else if (month == "6") { month = "Jul" }

        else if (month == "7") { month = "Aug" }

        else if (month == "8") { month = "Sep" }

        else if (month == "9") { month = "Oct" }

        else if (month == "10") { month = "Nov" }

        else if (month == "11") { month = "Dec" }


        if (dayNo < 10) { dayNo = '0' + dayNo }

        date = dayNo + "-" + month + "-" + year

        return date;

        setTimeout('DigiDate()', 1000)
    }
}

//======================================================================================================
function CompareDates(day1, mon1, yr1, day2, mon2, yr2, object1, object2, dt1, dt2) {
    var fromDate = new Date(day1 + " " + mon1 + " " + yr1);
    var toDate = new Date(day2 + " " + mon2 + " " + yr2);

    var date1 = fromDate.getMonth() + "/" + fromDate.getDate() + "/" + fromDate.getYear();
    var date2 = toDate.getMonth() + "/" + toDate.getDate() + "/" + toDate.getYear();
    if (Date.parse(date1) < Date.parse(date2)) {
        alert(dt1 + ' can not be before ' + dt2);
        object1.focus();
        return false;
    }
    else
        return true
}
//==================================
//Function Added By Biswaranjan on 30-sept-2010 to Get the default focus of the control while page loading
var ctrlid = null;
function DefaultFocus(controlid) {

    ctrlid = controlid;

    setTimeout("FocusFunction()", 1000);
}

function FocusFunction() {

    document.getElementById(ctrlid).focus();
    return false;
}
//====================================  
//Added By Biswaranjan on 31-sept-2010
//Function to fetch the whitespace and give and alert
function Chkwhitespace(ctrl, msg) {
    var ctrlval = document.getElementById(ctrl).value;
    var startval = 0;
    var objarry = new Array();
    objarry = ctrlval.split(' ');
    if (objarry[1] != null && objarry[1] != 'undefined') {
        alert('' + msg + ' contains white space');
        document.getElementById(ctrl).focus();
        return false;
    }
    else
        return true;
}
//====================================

//Created By Biswaranjan Das on 29-dec-2010 to print a grid of a page

function PrintPage(pageheader) {
    var wPageHeader = pageheader
    var curDate;
    curDate = new Date();
    curDate = "Date :" + curDate.getDate() + "/" + (curDate.getMonth() + 1) + "/" + curDate.getYear();

    var wOption = "width=875,height=525,menubar=yes,scrollbars=yes,location=no,left=20,top=20";
    var wWinHTML = document.getElementById('printArea').innerHTML;
    var wWinPrint = window.open("", "", wOption);

    wWinPrint.document.open();
    wWinPrint.document.write("<html><head><link href='../Styles/Print.css' rel='stylesheet'><title>e-Despatch</title></head><body>");
    wWinPrint.document.write("<div id='header'><div style='float:left; margin-top:10px;' width='220'><img src=../images/edespatchlg.gif alt=e-Despatch /></div><div style='float:right' align='right'><div align='right' id='printDate'>" + curDate + "</div></div><div style='clear:both'></div></div>")
    wWinPrint.document.write("<div align='center' id='printHeader'>" + wPageHeader + "</div>");
    //wWinPrint.document.write("<div align='right' id='printDate'>"+curDate+"</div>");	
    wWinPrint.document.write(wWinHTML);
    wWinPrint.document.write("</body></html>");
    wWinPrint.document.close();
    wWinPrint.focus();
}

//========== Text Counter (For Details Area)===========
//Added by Vivekananda Paital on 28-Feb-2011
function TextCounter(ctlTxtName, lblCouter, numTextSize) {
    var txtName = document.getElementById(ctlTxtName).value;
    var txtNameLength = txtName.length;
    if (parseInt(txtNameLength) > parseInt(numTextSize)) {
        var txtMaxTextSize = txtName.substr(0, numTextSize)
        document.getElementById(ctlTxtName).innerHTML = txtMaxTextSize;
        alert("Entered Text Exceeds '" + numTextSize + "' Characters.");
        document.getElementById(lblCouter).innerHTML = 0;
        return false;
    }
    else {
        document.getElementById(lblCouter).innerHTML = parseInt(numTextSize) - parseInt(txtNameLength);
        return true;
    }
}


//========== block specialcharacter first place =================
function blockspecialchar_first(e) {
    var str;
    str = e.value;

    switch (str.charCodeAt(0)) {
        case 44:
            {
                alert(", Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 45:
            {
                alert("- Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 47:
            {
                alert("/ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 58:
            {
                alert(": Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 46:
            {
                alert(". Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 35:
            {
                alert("# Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 38:
            {
                alert("& Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 39:
            {
                alert("Single Quote Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 32:
            {
                alert("White Space Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 33:
            {
                alert("! Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 34:
            {
                alert(" Double Quotes is Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 36:
            {
                alert("$ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 37:
            {
                alert("% Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 40:
            {
                alert("( Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 41:
            {
                alert(") Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 42:
            {
                alert("* Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 43:
            {
                alert("+ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 59:
            {
                alert("; Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 61:
            {
                alert("= Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 60:
            {
                alert("< Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 62:
            {
                alert("> Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 64:
            {
                alert("@ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 91:
            {
                alert("[ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 92:
            {
                alert("\ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 93:
            {
                alert("] Not allowed in 1st Place!!!");
                //e.value = "";
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 94:
            {
                alert("^ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 95:
            {
                alert("_ Not allowed in 1st Place!!!");
                //e.value = "";
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 96:
            {
                alert("' Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 123:
            {
                alert("{ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 124:
            {
                alert("| Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 125:
            {
                alert("} Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 126:
            {
                alert("~ Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
        case 63:
            {
                alert("? Not allowed in 1st Place!!!");
                str = str.substring(1, str.length);
                e.value = str;
                e.focus();
                return false;
            }
    }
}

// Added by Vivekananda Paital on 1-Mar-2011 to validate the date fields
//-----------------------------------------------------------------------------------------
function CurrentDateLessValidator(objCtl, Message)   //Given Date cannot be less than Current Date
{
    var StartDate = formatDate4(document.getElementById(objCtl).value);
    var Curdate = formatDate4(DigiDate4());
    if (!CheckDate4(Curdate, StartDate)) {
        alert(Message + " Can't Be Greater than Current Date");
        return false;
    }
    return true;
}
function CurrentDateGreaterValidator(objCtl, Message)  //Given Date cannot be less than Current Date
{
    var StartDate = formatDate4(document.getElementById(objCtl).value);
    var Curdate = formatDate4(DigiDate4());
    if (!CheckDate4(StartDate, Curdate)) {
        alert(Message + " Can't Be before Current Date");
        return false;
    }
    return true;
}

function CompareTwoDate(Controlname1, Controlname2, Fieldname1, Fieldname2) {
    debugger;
    var obj1 = $(Controlname1).val();
    var obj2 = $(Controlname2).val();
    var objFieldname1 = Fieldname1;
    var objFieldname2 = Fieldname2;
    var fromDate = obj1.value;
    var toDate = obj2.value;
    var dt1 = fromDate.split("/")
    var dt2 = toDate.split("/")
    var flag = false;
    var objValidator = new InputValidator();
    var objError = new ErrorAlert();

    if (fromDate != dt1 && toDate != dt2) {
        flag = objValidator.isDateCompareinCalendar(dt1[0], dt1[1], dt1[2], dt2[0], dt2[1], dt2[2], obj1, obj2, objValidator.Replace(objValidator.Replace(objError.strCompareDate, "<Field2>", objFieldname2), "<Field1>", objFieldname1))
    }
    else {
        dt1 = fromDate.split("-")
        dt2 = toDate.split("-")
        flag = objValidator.isDateCompareinCalendar(dt1[0], dt1[1], dt1[2], dt2[0], dt2[1], dt2[2], obj1, obj2, objValidator.Replace(objValidator.Replace(objError.strCompareDate, "<Field2>", objFieldname2), "<Field1>", objFieldname1))
    }
    return flag;
}
//-------------------------------------Compare Dates in Calendar Format(in dd-MMM-yyyy Format)-----------------------
function isDateCompareinCalendar(day1, mon1, yr1, day2, mon2, yr2, object1, object2, msg) {
    var fromDate = new Date(day1 + " " + mon1 + " " + yr1);
    var toDate = new Date(day2 + " " + mon2 + " " + yr2);
    var date1 = fromDate.getMonth() + "/" + fromDate.getDate() + "/" + fromDate.getYear();
    var date2 = toDate.getMonth() + "/" + toDate.getDate() + "/" + toDate.getYear();

    if (Date.parse(date1) > Date.parse(date2)) {
        alert(msg);
        object1.focus();
        return false;
    }
    else
        return true
}

//--------------------------Avoid multiple entries with multiple clicks in submit button-----------------------

//function disableBtn(btnID, newText) {

//    var btn = $(btnID);
//        setTimeout("setImage('"+btnID+"')", 10);
//        btn.disabled = true;
//        btn.value = newText;
//}

//function setImage(btnID) {
//    var btn = $(btnID);
//    btn.style.background = 'url(../images/Submiting.jpg)';
//}

//--------------------------//