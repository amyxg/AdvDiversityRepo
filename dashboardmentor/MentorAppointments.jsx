import React, { useState, useEffect } from 'react';
import appointmentService from '../../services/appointmentsService';
import MentorAppointmentCard from './MentorAppointmentCard';
import SimpleBar from 'simplebar-react';
import { Tab, Card, Nav, Row } from 'react-bootstrap';
import Calendly from "../calendly/Calendly"
import PropTypes from 'prop-types';
import './mentorappointments.css';
import * as toastr from 'toastr';
import 'toastr/build/toastr.css';

const MentorAppointments = (props) => {
    useEffect(() => {
        appointmentService.getAllMentorAppts().then(onGetAppointmentsSuccess).catch(onGetAppointmentsError);
    }, []);

    const [appts, setAppt] = useState({
        apptData: {
            old: [],
            upcoming: [],
        },
        mappedApptData: [],
    });

    const onGetAppointmentsSuccess = (response) => {
        setAppt((prevState) => {
            let pState = { ...prevState };
            let pAppts = response.items;
            pAppts.forEach((appt) => {
                const date = appt.apptDateTime;

                if (new Date(date) >= new Date()) {
                    pState.apptData.upcoming.push(appt);
                } else pState.apptData.old.push(appt);
            });
            pState.apptData.old.sort((a, b) => new Date(b.apptDateTime) - new Date(a.apptDateTime));
            pState.apptData.upcoming.sort((a, b) => new Date(a.apptDateTime) - new Date(b.apptDateTime));
            pState.mappedOld = pState.apptData.old.map(mapAppointment);
            pState.mappedUpcoming = pState.apptData.upcoming.map(mapAppointment);
            return pState;
        });
    };
    const onGetAppointmentsError = () => {
        toastr.error('Could not retrieve appointments');
    };

    const mapAppointment = (appt) => {
        return (
            <MentorAppointmentCard
                key={appt.id}
                appts={appt}
                currentUser={props.currentUser}
                onDeleteAppt={onDeleteAppt}></MentorAppointmentCard>
        );
    };

    const onDeleteAppt = (apptId, mentorId) => {
        appointmentService.deleteAppt(apptId, mentorId).then(onDeleteApptSuccess).catch(onDeleteApptError);
    };

    const onDeleteApptSuccess = () => {
        toastr.success('Appt Canceled, Please refresh page!');
    };
    const onDeleteApptError = () => {
        toastr.error('Error deleting appt!');
    };
    return (
        <React.Fragment>
            <Tab.Container defaultActiveKey="upcomingAppts">
                <Card>
                    <Card.Body>
                        <Nav variant="pills" className="nav nav-pills bg-nav-pills nav-justified mb-3">
                            <Nav.Item className="nav-item">
                                <Nav.Link href="#" eventKey="upcomingAppts" className="nav-link rounded-0">
                                    Upcoming Appointments
                                </Nav.Link>
                            </Nav.Item>
                            <Nav.Item className="nav-item">
                                <Nav.Link href="#" eventKey="pastAppts" className="nav-link rounded-0">
                                    Past Appointments
                                </Nav.Link>
                            </Nav.Item>
                            <Nav.Item className='nav-item'>
                                <Nav.Link href='#' eventKey="calendly" className='nav-link rounded-0'>
                                    Create Appointment
                                </Nav.Link>
                            </Nav.Item>
                        </Nav>

                        <Tab.Content>
                            <Tab.Pane eventKey="upcomingAppts">
                                <SimpleBar className="upcomingAppts">
                                    <Row>
                                        <h2 className="text-secondary m-2">Upcoming Appointments</h2>
                                        {appts.mappedUpcoming && appts.mappedUpcoming}
                                        {!appts.mappedUpcoming && <h3>Loading Appointments...</h3>}
                                    </Row>
                                </SimpleBar>
                            </Tab.Pane>
                        </Tab.Content>

                        <Tab.Content>
                            <Tab.Pane eventKey="pastAppts">
                                <SimpleBar className="pastAppts">
                                    <Row>
                                        <h2 className="text-secondary m-2">Past Appointments</h2>
                                        {appts.mappedOld && appts.mappedOld}
                                        {!appts.mappedOld && <h3>Loading Appointments...</h3>}
                                    </Row>
                                </SimpleBar>
                            </Tab.Pane>
                        </Tab.Content>
                        <Tab.Content>
                            <Tab.Pane eventKey="calendly">
                                <Calendly ownerUrl={props.userUrl} />
                            </Tab.Pane>
                        </Tab.Content>
                    </Card.Body>
                </Card>
            </Tab.Container>
        </React.Fragment>
    );
};

MentorAppointments.propTypes = {
    currentUser: PropTypes.shape({
        email: PropTypes.string,
        id: PropTypes.number.isRequired,
        isLoggedIn: PropTypes.bool,
        roles: PropTypes.arrayOf(PropTypes.string),
    }),
    userUrl: PropTypes.string
};
export default MentorAppointments;
