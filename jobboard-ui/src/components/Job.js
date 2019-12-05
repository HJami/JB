import React, { Component } from 'react';
import posed from 'react-pose';
import './Job.css';

const Box = posed.div({
  hoverable: true,
  dragBounds: { left: '-100%', right: '100%' },
  init: { scale: 1 },
  hover: { scale: 1.05 }
})

class Job extends Component {

  render() {
    console.log(this.props);
    return (<Box className="Job-Box">
      <div id="job_name">
        <b>job name: </b>{this.props.job.name}
      </div>
      <div id="job_location">
        <b>Location: </b>{this.props.job.location}
      </div>
      <div id="job_description">
        <span >
          <b>Description: </b>{this.props.job.description}
        </span >
      </div>
    </Box>);

  }
};

export default Job;
