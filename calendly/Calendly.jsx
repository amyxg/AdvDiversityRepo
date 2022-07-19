import React from 'react';
import { InlineWidget, useCalendlyEventListener } from 'react-calendly';
import PropTypes from "prop-types"
import 'toastr/build/toastr.css';

import debug from 'sabio-debug';
const _logger = debug.extend('calendly');
const _loggerCalendly = _logger.extend('calendlyWidget');

const Calendly = ({ ownerUrl }) => {


    useCalendlyEventListener({
        onProfilePageViewed: () => _loggerCalendly('onProfilePageViewed'),
        onDateAndTimeSelected: () => _loggerCalendly('onDateAndTimeSelected'),
        onEventTypeViewed: () => _loggerCalendly('onEventTypeViewed'),

    });



    return (
        <div className="App">
            {!ownerUrl && <h3>Loading...</h3>}
            {ownerUrl && <InlineWidget url={`${ownerUrl}`} />}
        </div>
    );
};

Calendly.propTypes = {
    ownerUrl: PropTypes.string.isRequired
}

export default Calendly;
