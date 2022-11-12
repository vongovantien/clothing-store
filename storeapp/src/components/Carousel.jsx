import React, { Component } from "react";
import Slider from "react-slick";

function SampleNextArrow(props) {
    const { className, style, onClick } = props;
    return (
        <div
            className={className}
            style={{ ...style, display: "block", background: "red" }}
            onClick={onClick}
        />
    );
}

function SamplePrevArrow(props) {
    const { className, style, onClick } = props;
    return (
        <div
            className={className}
            style={{ ...style, display: "block", background: "green" }}
            onClick={onClick}
        />
    );
}

export default class CustomArrows extends Component {
    render() {
        const settings = {
            dots: true,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2000,
            cssEase: "linear",
            fade: true,
        };

        const images = [{
            id: 1,
            url: "/img/banner-1.png"
        },
        {
            id: 2,
            url: "/img/banner-2.jpg"
        },
        {
            id: 3,
            url: "img/banner-3.jpg"
        }]
        return (
            <div>
                <Slider {...settings}>
                    {!!images && images.map(item =>
                        <div key={item.id}>
                            <img src={item.url} alt={item.id} style={{ width: "100%" }} />
                        </div>
                    )}
                </Slider>
            </div>
        );
    }
}