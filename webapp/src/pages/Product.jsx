import { Grid, Pagination } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import ItemCard from '../components/ItemCard';
import NavLeft from '../components/NavLeft';
import SearchForm from '../components/SearchForm';
import api, { endpoint } from '../endpoints/api';

const Product = () => {
    const [productList, setProductList] = useState([]);
    const [numberPage, setNumberPage] = useState(1);
    const [pageSize, setPageSize] = useState(5);

    useEffect(() => {
        getAllProduct();
    }, [numberPage])

    const getAllProduct = async () => {
        try {
            let result = await api.get(endpoint.product(numberPage, pageSize));
            setProductList(result.data)
        } catch (error) {
            console.log(error);
        }
    };

    const handlePaging = async (event, value) => {
        try {
            setNumberPage(value);
        } catch (error) {
            console.log(error)
        }
    }

    return (
        <div>
            <Container maxWidth="xl">
                <h1 style={{ textAlign: "center" }}>Danh sách sản phẩm</h1>
                <Grid container spacing={2} style={{ marginTop: "80px" }}>
                    <Grid item xs={4}>
                        <NavLeft />
                    </Grid>
                    <Grid item xs={8}>
                        <SearchForm />
                        <Grid
                            container
                            spacing={4}
                            justifyItems="center"
                        >
                            {productList && productList.map((item, index) =>
                                <Grid key={index} item xs={12} sm={6} md={4}>
                                    <ItemCard prop={item} />
                                </Grid>
                            )}
                        </Grid>
                        <Pagination count={10} color="primary"
                            style={{ padding: "20px 0", margin: "0 auto" }}
                            page={numberPage} onChange={handlePaging} />
                    </Grid>

                </Grid>
            </Container>
        </div>
    )
}

export default Product
