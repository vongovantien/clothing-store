import { TabContext, TabList, TabPanel } from '@mui/lab';
import { Box, Grid, Pagination, Tab } from '@mui/material';
import { Container } from '@mui/system';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import CustomArrows from '../components/Carousel';
import ItemCard from '../components/ItemCard';
import { products } from '../fakeData';
import { getHotProduct } from '../features/products/productSlice';

export const Home = () => {
	const [productList, setProductList] = useState([]);
	const dispatch = useDispatch();
	const [pageSize, setPageSize] = useState(4)
	const [pageNumber, setPageNumber] = useState(1)

	useEffect(() => {
		getHotProductList();
	}, [])

	const getHotProductList = async () => {
		dispatch(getHotProduct({ pageSize, pageNumber }))
			.unwrap()
			.then((res) => {
				setProductList(res)
			})
			.catch(res => console.error(res))
	};
	const onHandlePaging = (evt, value) => {
		console.log(value)

	}
	//tab
	const [value, setValue] = React.useState('1');

	const handleChange = (event, newValue) => {
		setValue(newValue);
	};

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
							{!!productList.data && productList.data.map(item =>
								<Grid key={item.id} item xs={12} sm={6} md={3}>
									<ItemCard prop={item} />
								</Grid>
							)}
							<Pagination count={productList.totalCount} color="primary" style={{ padding: "20px 0", margin: "0 auto" }} onChange={onHandlePaging} />
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
						</Grid>
						</TabPanel>
						<TabPanel value="3">
							<Grid container spacing={2}>
								<Grid item xs={12}>
									<Grid
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
					</TabContext>
				</Box>
			</Container>
		</>
	)
}
