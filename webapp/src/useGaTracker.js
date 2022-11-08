import { useEffect, useState } from "react";
import ReactGA from "react-ga";
import { useLocation } from "react-router-dom";

const useGaTracker = () => {
    const location = useLocation();
    const [initialized, setInitialized] = useState(false);

    useEffect(() => {
        if (!window.location.href.includes("localhost")) {
            ReactGA.initialize("G-SYHPRG8TWC");
        }
        setInitialized(true);
    }, []);

    useEffect(() => {
        if (initialized) {
            ReactGA.pageview(location.pathname + location.search);
        }
    }, [initialized, location]);
};

export default useGaTracker;