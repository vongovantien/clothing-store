import { TabContext, TabList, TabPanel } from '@mui/lab';
import { Box, Grid, Pagination, Tab } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import CustomArrows from '../components/Carousel';
import ItemCard from '../components/ItemCard';
import { getHotProduct } from '../features/products/productSlice';

export const Home = () => {
	console.log("abc");
	const [hotProductList, setHotProductList] = useState([]);
	const [productByTypeList, setProductByTypeList] = useState([]);
	const dispatch = useDispatch();
	const [pageSize, setPageSize] = useState(4)
	const [pageNumber, setPageNumber] = useState(1)

	//tab
	const [productType, setProductType] = useState('sale-product');

	const handleChange = (event, newValue) => {
		setProductType(newValue);
	};
	useEffect(() => {
		getHotProductList();
	}, [pageNumber])

	useEffect(() => {
		getProductByType();
	}, [productType])

	const getHotProductList = async () => {
		dispatch(getHotProduct({ pageSize, pageNumber }))
			.unwrap()
			.then((res) => {
				setHotProductList(res)
			})
			.catch(res => console.error(res))
	};

	const getProductByType = async () => {
		dispatch(getHotProduct({ pageSize, pageNumber }))
			.unwrap()
			.then((res) => {
				setProductByTypeList(res.data)
			})
			.catch(res => console.error(res))
	};
	const onHandlePaging = (evt, value) => {
		setPageNumber(value)
	}

	return (
		<>
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
							{!!hotProductList.data && hotProductList.data.map(item =>
								<Grid key={item.id} item xs={12} sm={6} md={3}>
									<ItemCard prop={item} />
								</Grid>
							)}
						</Grid>
					</Grid>
					<Pagination count={hotProductList.totalPages} color="primary" style={{ padding: "20px 0", margin: "0 auto" }} onChange={onHandlePaging} />
				</Grid>
				<Box sx={{ width: '100%', typography: 'body1' }}>
					<TabContext value={productType}>
						<Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
							<TabList onChange={handleChange} aria-label="lab API tabs example">
								<Tab label="New Product" value="new-product" />
								<Tab label="OnSale" value="sale-product" />
								<Tab label="Feature Product" value="feature-product" />
							</TabList>
						</Box>
						<TabPanel value="new-product">
							<Grid container spacing={2}>
								<Grid item xs={12}>
									<Grid
										container
										spacing={4}
										justifyItems="center"
										style={{ marginTop: "10px" }}
									>
										{!!productByTypeList.data && productByTypeList.data.map(item =>
											<Grid key={item.id} item xs={12} sm={6} md={3}>
												<ItemCard prop={item} />
											</Grid>
										)}
									</Grid>
								</Grid>
							</Grid>
						</TabPanel>
						<TabPanel value="sale-product"><Grid container spacing={2}>
							<Grid item xs={12}><Grid
								container
								spacing={4}
								justifyItems="center"
								style={{ marginTop: "10px" }}
							>
								{!!productByTypeList.data && productByTypeList.data.map(item =>
									<Grid key={item.id} item xs={12} sm={6} md={3}>
										<ItemCard prop={item} />
									</Grid>
								)}
							</Grid>
							</Grid>
						</Grid>
						</TabPanel>
						<TabPanel value="feature-product">
							<Grid container spacing={2}>
								<Grid item xs={12}>
									<Grid
										container
										spacing={4}
										justifyItems="center"
										style={{ marginTop: "10px" }}
									>
										{!!productByTypeList.data && productByTypeList.data.map(item =>
											<Grid key={item.id} item xs={12} sm={6} md={3}>
												<ItemCard prop={item} />
											</Grid>
										)}
									</Grid>
								</Grid>
							</Grid>
						</TabPanel>
					</TabContext>
				</Box>
			</Container>
		</>
	)
}
