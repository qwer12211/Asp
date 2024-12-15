document.getElementById("applyFilters").addEventListener("click", function () {
    const searchInput = document.getElementById("searchInput").value.toLowerCase();
    const selectedCategory = document.getElementById("filterCategory").value;
    const sortOption = document.getElementById("sortOptions").value;

    const products = Array.from(document.querySelectorAll(".product-item"));

    products.forEach(product => {
        const name = product.getAttribute("data-name").toLowerCase();
        const category = product.getAttribute("data-category");

        const matchesCategory = selectedCategory === "" || category === selectedCategory;
        const matchesSearch = name.includes(searchInput);

        if (matchesCategory && matchesSearch) {
            product.style.display = "block";
        } else {
            product.style.display = "none";
        }
    });

    const visibleProducts = products.filter(product => product.style.display !== "none");
    visibleProducts.sort((a, b) => {
        const priceA = parseFloat(a.getAttribute("data-price"));
        const priceB = parseFloat(b.getAttribute("data-price"));
        const nameA = a.getAttribute("data-name").toLowerCase();
        const nameB = b.getAttribute("data-name").toLowerCase();

        switch (sortOption) {
            case "priceAsc":
                return priceA - priceB;
            case "priceDesc":
                return priceB - priceA;
            case "nameAsc":
                return nameA.localeCompare(nameB);
            case "nameDesc":
                return nameB.localeCompare(nameA);
        }
    });

    const productsList = document.getElementById("productsList");
    visibleProducts.forEach(product => {
        productsList.appendChild(product);
    });
});