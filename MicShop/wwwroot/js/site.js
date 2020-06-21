// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// product class
var ProductEntity = function () {
    let _self = this;
    _self.Id = null;
    _self.Quantity = 0;
    _self.Price = 0;
}

// basketlist class
let BasketListEntity = function () {
    let _self = this;
    _self.ProductList = null;
    _self.bringPrice = 500;

    _self.Init = function () {
        let _basketlist = localStorage.getItem("basketList");
        if (_basketlist == null || _basketlist == "null" || _basketlist == "") {
            _self.ProductList = []
        }
        else {
            _self.ProductList = JSON.parse(_basketlist);
        }


        Save();
    }
    _self.GetNum = function (id) {
        for (let i in _self.ProductList) {
            if (_self.ProductList[i].Id == id) {
                return _self.ProductList[i].Quantity;
            }
        }
        
        return 1;
    }

    function InitValues() {
        $(".basketNum").each(function (e) {
            $(this).html(_self.ProductList.length);
        });
    }

    _self.Add = function (Product) {
        for (var i in _self.ProductList) {
            let curentProduct = _self.ProductList[i];
            if (Product.Id === curentProduct.Id) {
                curentProduct.Quantity += Product.Quantity;
                Save();
                return;
            }
        }

        _self.ProductList.push(Product);

        Save();
    }

    _self.Remove = function (id) {

    }

    function Save() {
        localStorage.setItem("basketList", JSON.stringify(_self.ProductList));
        InitValues();

        _self.CalculateTotalPrice();
    }

    _self.Rem = function (id) {
        for (var i = 0; i < _self.ProductList.length; i++) {
            if (_self.ProductList[i].Id == id) {
                _self.ProductList.splice(i, 1);
            }
        }

        Save();
    }

    _self.GetIds = function () {
        let list = [];
        for (let i in _self.ProductList) {
            list.push(_self.ProductList[i].Id);
        }
        return list;
    }

    _self.Edit = function (id, num) {
        for (let i in _self.ProductList) {
            if (_self.ProductList[i].Id == id) {
                _self.ProductList[i].Quantity = num;
            }
        }
        Save();
    }

    _self.BindEvents = function () {
        $(".addProductNum").unbind().bind('click', function (e) {
            let id = $(this).data('id');
            var hiddenCount = $('#curItemHiddenCount' + id);
            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#basketProductNum" + id);
                let newVal = parseInt(prodNumElem.val()) + 1;
                if (newVal >= 0) {
                    hiddenCount.val(newVal);
                    prodNumElem.val(newVal);
                    baskets.CalculatePrice(id);

                    _self.Edit(id, newVal);
                }
            }
        });
        $(".remProductNum").unbind().bind('click', function (e) {
            let id = $(this).data('id');

            var hiddenCount = $('#curItemHiddenCount' + id);

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#basketProductNum" + id);
                let newVal = parseInt(prodNumElem.val()) - 1;
                if (newVal >= 0) {
                    hiddenCount.val(newVal);
                    prodNumElem.val(newVal);
                    baskets.CalculatePrice(id);

                    _self.Edit(id, newVal);
                }
            }
        });

        $('.backetNumInput').unbind().bind('change keyup', function () {
            let id = $(this).data('id');

            var hiddenCount = $('#curItemHiddenCount' + id);

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#basketProductNum" + id);
                let newVal = parseInt($(this).val());
                if (newVal >= 0) {
                    hiddenCount.val(newVal);
                    prodNumElem.val(newVal);
                    baskets.CalculatePrice(id);

                    _self.Edit(id, newVal);
                }
            }
        })

        $(".remProduct").unbind().bind('click', function (e) {
            e.preventDefault();
            let id = $(this).data('id');

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#curProduct" + id);
                prodNumElem.remove();

                baskets.Rem(id);
            }
        });

        for (let i in _self.ProductList) {
            _self.CalculatePrice(_self.ProductList[i].Id);
        }

        _self.CalculateTotalPrice();
    }

    _self.CalculatePrice = function (id) {
        let prodNumElem = $("#basketProductNum" + id);
        let num = prodNumElem.val();
        num = parseInt(num);

        let price = parseInt($('#basketProductPrice' + id).val());

        $('#basketProductTotalPrice' + id).html((price * num) + '<i class="dram">AMD</i>');
    }

    _self.CalculateTotalPrice = function () {
        let totalPrice = 0;

        for (let i in _self.ProductList) {
            let id = _self.ProductList[i].Id;

            let prodTotal = $('#TotalItem\\[' + i + '\\]');
         

            let price = parseInt(prodTotal.text());
            
            totalPrice += price;
        }

        $(".totalPrice").html(totalPrice);

        if (totalPrice > 5000) {
            _self.bringPrice = 0;
        }
        else
            _self.bringPrice = 500;

        $(".bringPrice").html(_self.bringPrice);
        $(".payPrice").html(parseInt(_self.bringPrice) + parseInt(totalPrice));
    }
}

// wishlist class
var WishListEntity = function () {
    let _self = this;
    _self.ProductList = null;

    _self.Init = function () {
        let _wishlist = localStorage.getItem("wishList");
        if (_wishlist == null) {
            _self.ProductList = []
        }
        else {
            _self.ProductList = JSON.parse(_wishlist);
        }

        Save();
    }

    function InitValues() {
        $(".wishNum").each(function (e) {
            $(this).html(_self.ProductList.length);
        });
    }

    _self.Add = function (Product) {
        for (var i in _self.ProductList) {
            let curentProduct = _self.ProductList[i];
            if (Product.Id === curentProduct.Id) {
                return false;
            }
        }

        _self.ProductList.push(Product);

        Save();
    }

    _self.Remove = function (id) {

    }

    _self.Edit = function (id, num) {
        for (let i in _self.ProductList) {
            if (_self.ProductList[i].Id == id) {
                _self.ProductList[i].Quantity = num;
            }
        }
        Save();
    }

    function Save() {
        localStorage.setItem("wishList", JSON.stringify(_self.ProductList));
        InitValues();
    }

    _self.Rem = function (id) {
        for (var i = 0; i < _self.ProductList.length; i++) {
            if (_self.ProductList[i].Id == id) {
                _self.ProductList.splice(i, 1);
            }
        }

        Save();
    }

    _self.GetIds = function () {
        let list = [];
        for (let i in _self.ProductList) {
            list.push(_self.ProductList[i].Id);
        }
        return list;
    }

    _self.BindEvents = function () {
        $(".addProductNum").unbind().bind('click', function (e) {
            let id = $(this).data('id');

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#wishProductNum" + id);
                let newVal = parseInt(prodNumElem.val()) + 1;
                if (newVal >= 0) {
                    prodNumElem.val(newVal);
                    wishes.CalculatePrice(id);
                }
            }
        });
        $(".remProductNum").unbind().bind('click', function (e) {
            let id = $(this).data('id');

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#wishProductNum" + id);
                let newVal = parseInt(prodNumElem.val()) - 1;
                if (newVal >= 0) {
                    prodNumElem.val(newVal);
                    wishes.CalculatePrice(id);
                }
            }
        });
        $(".remProduct").unbind().bind('click', function (e) {
            e.preventDefault();
            let id = $(this).data('id');

            if (typeof id !== "undefined" && id !== null) {
                let prodNumElem = $("#curProduct" + id);
                prodNumElem.remove();

                wishes.Rem(id);
            }
        });

        for (let i in _self.ProductList) {
            _self.CalculatePrice(_self.ProductList[i].Id);
        }
    }

    _self.CalculatePrice = function (id) {
        let prodNumElem = $("#wishProductNum" + id);
        let num = prodNumElem.val();
        num = parseInt(num);

        let price = parseInt($('#wishProductPrice' + id).val());

        $('#wishProductTotalPrice' + id).html((price * num) + '<i class="dram" >AMD</i>');
    }
}

// wishlist instance
var wishes = new WishListEntity();
wishes.Init();

// basket instance
var baskets = new BasketListEntity();
baskets.Init();

$(document).ready(function () {

    var menuOpenned = false;

    $(".burger-menu-icon").on("click", function () {
        if (!menuOpenned) {
            $(".floating-nav").animate({ height: "100%" }, 300, "linear");
            $(".burger-menu").slideDown(500);
            menuOpenned = true
        } else {
            $(".floating-nav").animate({ height: "80px" }, 300, "linear");
            $(".burger-menu").slideUp(500);
            menuOpenned = false;
        }
    })

    $(".left-sidebar-menu").find(".sidebar-dropdown-opener").click(function (e) {
        e.preventDefault();
        $(this).toggleClass("active");
    });

    $(".wishCount")


    $(".wishItem").unbind().bind('click', function (e) {
        let Product = new ProductEntity();
        Product.Id = $(this).data('id');

        wishes.Add(Product);

    });
    $(".basketItem").unbind().bind('click', function (e) {
        let Product = new ProductEntity();
        Product.Id = $(this).data('id');
        Product.Quantity = 1;
        baskets.Add(Product);

    });
    $(".remProduct").unbind().bind('click', function (e) {
        console.log("papa");
        e.preventDefault();
        let id = $(this).data('id');

        if (typeof id !== "undefined" && id !== null) {
            baskets.Rem(id);
        }
    });

    $("#AddToBasket").unbind().bind('click', function (e) {
        e.preventDefault();

        $('.productItems').each(function (e) {
            let id = $(this).data('id');

            let prodNumElem = $("#wishProductNum" + id);
            let num = prodNumElem.val();
            num = parseInt(num);

            let Product = new ProductEntity();
            Product.Id = id;
            Product.Quantity = num;
            baskets.Add(Product);
        })
        console.log("adding")
    });

    var menuItems = $("#menu-content").find(".col-sm-12 > ul > li");
    var menuCount = menuItems.length;
    if (menuCount > 6) {
        for (var m = 6; m < menuCount; m++) {
            $(menuItems[m]).addClass("drops-left");
        }
    }
})