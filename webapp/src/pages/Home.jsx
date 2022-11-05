import { TabContext, TabList, TabPanel } from '@mui/lab';
import { Box, Grid, Pagination, Tab } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import CustomArrows from '../components/Carousel';
import ItemCard from '../components/ItemCard';
import api, { endpoint } from '../endpoints/api';
import { products } from '../fakeData';


export const Home = () => {
    const [product, setProductt] = useState("");
    const [productList, setProductList] = useState([]);

    useEffect(() => {
        getAllProduct();
    }, [])

    const getAllProduct = async () => {
        try {
            let result = await api.get(endpoint.product);
            setProductList(result.data)
        } catch (error) {
            console.log(error);
        }
    };

    //tab
    const [value, setValue] = React.useState('1');

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <>
            <div>
                <input
                    value={product}
                    onChange={(e) => setProductt(e.target.value)}
                />
            </div>



            --------------------------------
            <Container maxWidth="xl">
                <Container>
                    <CustomArrows />
                </Container>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <h1 style={{ textAlign: "center" }}>Top sản phẩm bán chạy trong tháng</h1>
                        <Grid
                            container
                            spacing={4}
                            justifyItems="center"
                            style={{ marginTop: "80px" }}
                        >
                            {productList && productList.map(item =>
                                <Grid key={item.id} item xs={12} sm={6} md={4}>
                                    <ItemCard prop={item} />
                                </Grid>
                            )}
                            <Pagination count={10} color="primary" style={{ padding: "20px 0", margin: "0 auto" }} />
                        </Grid>
                    </Grid>
                </Grid>
                <Box sx={{ width: '100%', typography: 'body1' }}>
                    <TabContext value={value}>
                        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                            <TabList onChange={handleChange} aria-label="lab API tabs example">
                                <Tab label="New Product" value="1" />
                                <Tab label="OnSale" value="2" />
                                <Tab label="Feature Product" value="3" />
                            </TabList>
                        </Box>

                        <TabPanel value="1">
                            <Grid container spacing={2}>
                                <Grid item xs={12}><Grid
                                    container
                                    spacing={4}
                                    justifyItems="center"
                                    style={{ marginTop: "10px" }}
                                >
                                    {products && products.map(item =>
                                        <Grid key={item.id} item xs={12} sm={6} md={3}>
                                            <ItemCard prop={item} />
                                        </Grid>
                                    )}
                                </Grid>
                                </Grid>
                            </Grid>
                        </TabPanel>
                        <TabPanel value="2"><Grid container spacing={2}>
                            <Grid item xs={12}><Grid
                                container
                                spacing={4}
                                justifyItems="center"
                                style={{ marginTop: "10px" }}
                            >
                                {products && products.map(item =>
                                    <Grid key={item.id} item xs={12} sm={6} md={3}>
                                        <ItemCard prop={item} />
                                    </Grid>
                                )}
                            </Grid>
                            </Grid>
                        </Grid></TabPanel>
                        <TabPanel value="3"><Grid container spacing={2}>
                            <Grid item xs={12}><Grid
                                container
                                spacing={4}
                                justifyItems="center"
                                style={{ marginTop: "10px" }}
                            >
                                {products && products.map(item =>
                                    <Grid key={item.id} item xs={12} sm={6} md={3}>
                                        <ItemCard prop={item} />
                                    </Grid>
                                )}
                            </Grid>
                            </Grid>
                        </Grid></TabPanel>
                    </TabContext>
                </Box>
            </Container>
        </>
    )
}
