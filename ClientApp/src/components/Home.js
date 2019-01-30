import React, { Component } from 'react';
import GoogleMapReact from 'google-map-react';
 
const AnyReactComponent = ({ text }) => <div>{text}</div>;
 
class SimpleMap extends Component {
  static defaultProps = {
    center: {
      lat: -36.840556,
      lng: 174.74
    },
    zoom: 15
  };
 
  render() {
    return (
      // Important! Always set the container height explicitly
      <div style={{ height: '100vh', width: '100%' }}>
        <GoogleMapReact
          bootstrapURLKeys={{ key: 'AIzaSyBn5olZpByo_sDdZkuJu6cvghnD47f8SxE' }}
          defaultCenter={this.props.center}
          defaultZoom={this.props.zoom}
        >
          <AnyReactComponent
            lat={-36.840556}
            lng={174.73}
            text={'Text'}
          />
          <AnyReactComponent
            lat={-36.840556}
            lng={174.75}
            text={'Some more text'}
          />
        </GoogleMapReact>
      </div>
    );
  }
}
 
export default SimpleMap;