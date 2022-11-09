import { Grid, Pagination } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import ItemCard from '../components/ItemCard';
import NavLeft from '../components/NavLeft';
import SearchForm from '../components/SearchForm';
import productService from '../services/product.service';
import { PAGE_SIZE, PRICE, SORT_BY } from '../utils/constants/ProductContants';

const Product = () => {
    const [productList, setProductList] = useState([]);
    const [numberPage, setNumberPage] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("Name ASC")
    const [price, setPrice] = useState("Name ASC")

    useEffect(() => {
        getAllProduct(pageSize, numberPage);
    }, [numberPage, pageSize, sortBy])

    const getAllProduct = async () => {
        try {
            let result = await productService.getProductPaging(pageSize, numberPage);
            setProductList(result.data);
        } catch (error) {
            console.log(error);
        }
    };

    const handleSearch = async (value, type) => {
        switch (type) {
            case PAGE_SIZE:
                setPageSize(value)
                break;
            case SORT_BY:
                setSortBy(value)
                break;
            case PRICE:
                console.log(value)
                setPrice(value)
                break;
            default:
                break;
        }
    }

    const handlePaging = (event, value) => {
        try {
            setNumberPage(value);
        }
        catch (error) {
            console.log(error)
        }
    }

    return (
        <div>
            <Container maxWidth="xl">
                <h1 style={{ textAlign: "center" }}>Danh sách sản phẩm</h1>
                <Grid container spacing={2} style={{ marginTop: "80px" }}>
                    <Grid item xs={4}>
                        <NavLeft price={price} handleSearch={handleSearch} />
                    </Grid>
                    <Grid item xs={8}>
                        <SearchForm pageSize={pageSize} sortBy={sortBy} handleSearch={handleSearch} />
                        <Grid
                            container
                            spacing={4}
                            justifyItems="center"
                        >
                            {!!productList.data && productList.data.map((item, index) =>
                                <Grid key={index} item xs={12} sm={6} md={4}>
                                    <ItemCard prop={item} />
                                </Grid>
                            )}
                        </Grid>
                        <Pagination count={productList.totalPages} color="primary"
                            style={{ padding: "20px 0", margin: "0 auto" }}
                            page={numberPage} onChange={handlePaging} />
                    </Grid>
                </Grid>
            </Container>
        </div>
    )
}

export default Product
